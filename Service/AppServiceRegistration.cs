using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class AppServiceRegistration
    {
        public static IServiceCollection AddServiceRegister(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { },
                typeof(Service.AssemblyRef).Assembly);

            services.AddScoped<IServiceManager, ServiceManager>();

            return services;

        }
    }
}
