using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;

namespace ECommerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            // without it ASP.NET doesn't know that
            // your project contain controllers

            builder.Services.AddEndpointsApiExplorer();
            // discover all API endpoints
            // each action in the controller is
            // considered an endpoint

            builder.Services.AddSwaggerGen();
            // convert discovered metadata [endpoints]
            // into OpenAPI specification [JSON]

            builder.Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //builder.Services.AddAutoMapper(X => X.AddProfile(new ProductProfile()));

            builder.Services.AddAutoMapper(cfg => { },
                typeof(Service.AssemblyRef).Assembly);

            builder.Services.AddScoped<IServiceManager, ServiceManager>();



            #endregion

            var app = builder.Build();

            #region use dataseeding before running the app

            using var Scope = app.Services.CreateScope();

            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await ObjectOfDataSeeding.DataSeedAsync();

            #endregion

            #region Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                // to expose the JSON [OpenAPI specification]
                // via HTTP

                app.UseSwaggerUI();
                // used to provide interactive swagger web page
                // reads the swagger JSON to generate UI
            }

            app.UseHttpsRedirection();
            // forces HTTP requests to use HTTPS

            app.UseStaticFiles();
            // to serve static files like images, CSS, JavaScript

            app.MapControllers();
            // connects the http routes to the controller actions 

            #endregion

            app.Run();
            // build the app , listen to requests
        }
    }
}
