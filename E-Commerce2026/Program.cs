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
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

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
            builder.Services.AddWebServices(builder.Configuration);
            
            
            
            
            

            #endregion

            var app = builder.Build();

            #region use dataseeding before running the app

            using var Scope = app.Services.CreateScope();

            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.IdentityDataSeedAsync();

            #endregion

            #region Configure the HTTP request pipeline.

            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                // to expose the JSON [OpenAPI specification]
                // via HTTP

                app.UseSwaggerUI(Options =>
                {
                    Options.ConfigObject = new ConfigObject()
                    {
                        DisplayRequestDuration = true
                    };

                    Options.DocumentTitle = "My E-Commerce API";

                    Options.JsonSerializerOptions = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    Options.DocExpansion(DocExpansion.None);
                    Options.EnableFilter();
                    Options.EnablePersistAuthorization();
                });
                // used to provide interactive swagger web page
                // reads the swagger JSON to generate UI
            }

            app.UseHttpsRedirection();
            // forces HTTP requests to use HTTPS

            app.UseStaticFiles();
            // to serve static files like images, CSS, JavaScript

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            // connects the http routes to the controller actions 

            #endregion

            app.Run();
            // build the app , listen to requests
        }
    }
}
