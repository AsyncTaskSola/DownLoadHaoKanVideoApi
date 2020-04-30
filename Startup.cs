using DownLoadHaoKanVideoAPI.Dbdata;
using DownLoadHaoKanVideoAPI.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DownLoadHaoKanVideoAPI
{
    //���������Ϣ���������https://lujiaxing.com/blogs/archives/1 ���Ⱥ�ѵ��ṩ
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<SampleDBContext>(option => { option.UseSqlite("Data Source=Download.db"); });
            InjectionSetting.SetServices(ref services);
            services.AddIdentityCore<Employee>()
                .AddUserStore<AccountManager>()
                .AddSignInManager<SignInManager<Employee>>()
                .AddUserManager<UserManager<Employee>>();
            //Ĭ�Ͻ�����ʽ
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;  //ע��: AddAuthentication(opt) => opt.DefaultAuthenticateScheme������.AddCookie() �ĵ�һ������һ��Ҫһ��.
                })                                       
                .AddCookie(IdentityConstants.ApplicationScheme, options =>
                {
                    options.LoginPath = "/api/account/forbidden";
                    options.AccessDeniedPath = "/api/account/forbidden";
                });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
