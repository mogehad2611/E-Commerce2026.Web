using DomainLayer.Contracts;
using E_Commerce2026.CustomMiddleWares;
using E_Commerce2026.Extentions;
using E_Commerce2026.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using Shared.ErrorModels;

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


            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddServiceRegister();
            builder.Services.AddWebServices();
            
            
            

            #endregion

            var app = builder.Build();

            #region use dataseeding before running the app

            using var Scope = app.Services.CreateScope();

            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await ObjectOfDataSeeding.DataSeedAsync();

            #endregion

            #region Configure the HTTP request pipeline.

            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

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
