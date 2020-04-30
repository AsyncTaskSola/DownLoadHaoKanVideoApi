using System;
using DownLoadHaoKanVideoAPI.Dbdata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DownLoadHaoKanVideoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var dbcontext = scope.ServiceProvider.GetService<SampleDBContext>();
                    //dbcontext.Database.EnsureDeleted();//ÿ������ɾ��
                    dbcontext.Database.Migrate();//ÿ��Ǩ��
                }
                catch (Exception e)
                {
                    var log = scope.ServiceProvider.GetService<ILogger<Program>>();
                    log.LogError(e, "database migration error!");
                }
            }
            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
