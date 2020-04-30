using System;
using DownLoadHaoKanVideoAPI.Interface;
using DownLoadHaoKanVideoAPI.Repository;
using DownLoadHaoKanVideoAPI.Servers;
using Microsoft.Extensions.DependencyInjection;

namespace DownLoadHaoKanVideoAPI.Entity
{
    public class InjectionSetting
    {
        public static void SetServices(ref IServiceCollection services)
        {
            services.AddScoped<IEmployeeServers, EmployeeServers>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
