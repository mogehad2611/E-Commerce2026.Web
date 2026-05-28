using E_Commerce2026.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce2026.Extentions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {

            Services.AddEndpointsApiExplorer();
            // discover all API endpoints
            // each action in the controller is
            // considered an endpoint

            Services.AddSwaggerGen();
            // convert discovered metadata [endpoints]
            // into OpenAPI specification [JSON]
            return Services;
        }

        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = APIResponseFactory.GenApiValidaionResponse;

            });



            return services;
        }
    }
}
