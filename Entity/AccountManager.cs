using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DownLoadHaoKanVideoAPI.Dbdata;
using Microsoft.AspNetCore.Identity;

namespace DownLoadHaoKanVideoAPI.Entity
{
    // <summary>
    // 账户管理
    // 一个用户管理的类.用于接管用户增删改查的功能 IUserStore 直接面对从数据库里出来的最原始的用户数据, UserManager 则是聚合用户数据的各种操作, 形成用户管理的   逻辑(改密码, 建用户, 等等...)
    // </summary>
    public class AccountManager : IUserStore<Employee>, IUserPasswordStore<Employee>
    {
        private SampleDBContext dbContext = null;
        private IPasswordHasher<Employee> _pwdHasher = null;
        public AccountManager(SampleDBContext context, IPasswordHasher<Employee> pwdHasher)
        {
            dbContext = context;
            _pwdHasher = pwdHasher;
        }

        /// <summary>
        /// 根据用户名查询用户
        /// </summary>
        public async Task<Employee> FindByNameAsync(string normalizedUserName,
            CancellationToken cancellationToken)
        {
            return await Task.Run<Employee>(() =>
            {
                var foundUser = dbContext
                    .Emplyees
                    .FirstOrDefault(p =>
                        p.UserName.ToUpper() == normalizedUserName
                        && p.Status == 1);
                return foundUser;
            }, cancellationToken);
        }

        /// <summary>
        /// 获取用户的密码哈希值
        /// </summary>
        public async Task<string> GetPasswordHashAsync(Employee user,
            CancellationToken cancellationToken)
        {
            string hashedPassword = _pwdHasher.HashPassword(user, user.Password);
            return await Task.FromResult(hashedPassword);
        }
        /// <summary>
        /// 读取用户ID
        /// </summary>
        public async Task<string> GetUserIdAsync(Employee user,
            CancellationToken cancellationToken)
        {
            return await Task.FromResult("" + user.id);
        }
        /// <summary>
        /// 读取用户名
        /// </summary>
        public async Task<string> GetUserNameAsync(Employee user,
            CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserName);
        }

        /// <summary>
        /// 添加用户 (不用了还要重新迁移继承: IdentityUser接口)
        /// </summary>
        /// <returns></returns>
        public Task<IdentityResult> CreateAsync(Employee user, CancellationToken cancellationToken)
        {
            //var data = await dbContext.Emplyees.AddAsync(user);
            //if (data != null)
            //{
            //    return IdentityResult.Success;
            //}

            //return IdentityResult.Failed(new IdentityError
            //{
            //    Description = "添加错误"
            //});
            throw new NotImplementedException();
        }

        #region 暂时用不上
        public Task<string> GetNormalizedUserNameAsync(Employee user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(Employee user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(Employee user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(Employee user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(Employee user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Employee user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<IdentityResult> DeleteAsync(Employee user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}
