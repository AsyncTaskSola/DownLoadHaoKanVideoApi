using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DownLoadHaoKanVideoAPI.Entity;
using DownLoadHaoKanVideoAPI.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DownLoadHaoKanVideoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _loginManager;
        private readonly IEmployeeServers _employeeServers;

        public AccountController(UserManager<Employee> userManager, SignInManager<Employee> loginManager,IEmployeeServers employeeServers)
        {
            _userManager = userManager;
            _loginManager = loginManager;
            _employeeServers = employeeServers;
        }

        [HttpPost]
        public async Task<ResultModel<Employee>> Login(LoginRequest req)
        {
            try
            {
                if (req == null) throw new Exception(nameof(req));
                if (!ModelState.IsValid)
                {
                    return new ResultModel<Employee> { State = ResultType.Error, Message = "登录信息不完整" };
                }
                var user = await _userManager.FindByNameAsync(req.UserName);
                if (user == null)
                {
                    return new ResultModel<Employee> { State = ResultType.Error, Message = "用户不存在" };
                }
                var hashed = MD5Encrypt(req.Password);
                var signInResult = await _loginManager.PasswordSignInAsync(user.UserName, hashed, false, false);
                //不能把用户的密码也传回去
                user.Password = null;
                if (signInResult.Succeeded)
                {
                    return new ResultModel<Employee> { State = ResultType.Success, Message = "登录成功", Data = user };
                }
                else
                {
                    return new ResultModel<Employee> { State = ResultType.Error, Message = "存在未知异常"};
                }
            }
            catch (Exception e)
            {
                return new ResultModel<Employee> { State = ResultType.Error, Message = "存在未知异常" };
            }

        }
        [HttpPost]
        public async Task<ResultModel<Employee>> CreatTask([FromForm]Employee employee)
        {
            if (employee == null) throw new Exception(nameof(employee));
            if (string.IsNullOrEmpty(employee.UserName)||string.IsNullOrEmpty(employee.Password)) 
                return new ResultModel<Employee> { State = ResultType.Error, Message = "账号或密码不能为空" };
            if (ModelState.IsValid)
            {
                employee.Password = MD5Encrypt(employee.Password);
                employee.Status = 1; 
                _employeeServers.AddEmployee(employee);
                //添加用户(不用了还要重新迁移继承: IdentityUser接口) 想了解自己看官网
                //result= await _userManager.CreateAsync(employee,employee.Password);
            }
            return new ResultModel<Employee> { State = ResultType.Success, Message = "账号创建成功", Data = employee };
        }


        [HttpGet]
        public async Task<ResultModel<string>> Forbidden(string returnUrl)
        {
            return await Task.FromResult(new ResultModel<string>()
            {
                Data = returnUrl,
                //ResponseCode = 2,
                Message = "未登录或者没有访问权限"
            });
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        private static string MD5Encrypt(string data)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            return string.Concat(hash.Select(p => p.ToString("x2").ToUpper()));
        }
    }
}
