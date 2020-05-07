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
            #region ����
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
            //��Ŀ��Ŀ¼
            var basePath = Directory.GetCurrentDirectory();
            //ʹ����Ŀ���ݵ�XML��������Ҫͨ����Ŀ���� 
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

                //����XMLע���ĵ�
                // �ڶ�����includeControllerXmlComments Ϊtrueʱ��������ʾ����ע��
                option.IncludeXmlComments(filePath, true);
                //����Ӷ��XML���뵵�� ����Ŀ�ֲ�������Ҫ
                //option.IncludeXmlComments(Path.Combine(basePath, "DownLoadHaoKanVideoAPI.xml"), true);
                // include document file
                // option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml"), true);
            });
            #endregion

            #region �����֤
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
            //        .AllowCredentials() //����cookies
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
