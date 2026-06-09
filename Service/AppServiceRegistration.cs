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

            services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<Func<IProductService>>(provider =>
            () => provider.GetRequiredService<IProductService>());

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<Func<IOrderService>>(provider =>
            () => provider.GetRequiredService<IOrderService>());

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<Func<IBasketService>>(provider =>
            () => provider.GetRequiredService<IBasketService>());

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>());


            return services;

        }
    }
}
