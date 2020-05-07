using System;
using System.IO;
using DownLoadHaoKanVideoAPI.Dbdata;
using DownLoadHaoKanVideoAPI.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DownLoadHaoKanVideoAPI
{
    //相关配置信息调动请参照https://lujiaxing.com/blogs/archives/1 这个群友的提供
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region 跨域
            services.AddCors(options =>
            {
                options.AddPolicy(name: "Policy1",
                    builder => {
                        builder.WithOrigins("http://127.0.0.1:8080", "http://localhost:8080")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            #endregion

            services.AddControllers();
            services.AddDbContext<SampleDBContext>(option => { option.UseSqlite("Data Source=Download.db"); });
            InjectionSetting.SetServices(ref services);

            #region swagger service
            //项目根目录
            var basePath = Directory.GetCurrentDirectory();
            //使用项目内容的XML解析，需要通过项目生成 
            var filePath = Path.Combine(basePath, "DownLoadHaoKanVideoAPI.xml");
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("VideoDownload", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "VideoDownload API",
                    Description = "API for VideoDownload",
                    License = new OpenApiLicense
                    {
                        Name = "Git AsyncTask(Evan)",
                        Url = new Uri("https://github.com/AsyncTaskSola/DownLoadHaoKanVideoApi.git"),
                    },
                    Contact = new OpenApiContact
                    {
                        Name = "Evan",
                        Email = "22955559393@qq.com",
                        Url = new Uri("https://mail.qq.com/"),
                    },
                });

                //加载XML注释文档
                // 第二参数includeControllerXmlComments 为true时控制器显示中文注释
                option.IncludeXmlComments(filePath, true);
                //可添加多份XML翻译档案 ，项目分布类所需要
                //option.IncludeXmlComments(Path.Combine(basePath, "DownLoadHaoKanVideoAPI.xml"), true);
                // include document file
                // option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml"), true);
            });
            #endregion

            #region 身份验证
            services.AddIdentityCore<Employee>()
                .AddUserStore<AccountManager>()
                .AddSignInManager<SignInManager<Employee>>()
                .AddUserManager<UserManager<Employee>>();
            //默认解锁方式
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;  //注意: AddAuthentication(opt) => opt.DefaultAuthenticateScheme跟后面.AddCookie() 的第一个参数一定要一致.
                })
                .AddCookie(IdentityConstants.ApplicationScheme, options =>
                {
                    options.LoginPath = "/api/account/forbidden";
                    options.AccessDeniedPath = "/api/account/forbidden";
                });
            #endregion
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            #region Cors
            app.UseCors("Policy1");
            //app.UseCors(option =>
            //{
            //    option.WithOrigins("http://127.0.0.1:8080", "http://localhost:8080")
            //        .AllowAnyHeader()
            //        .AllowCredentials() //允许cookies
            //        .WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS");
            //});
            #endregion

            #region Swagger
            //Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            //Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/VideoDownload/swagger.json", "VideoDownload Docs");

                option.RoutePrefix = string.Empty;
                option.DocumentTitle = "VideoDownload API by Author Evan";
            });
            #endregion
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
