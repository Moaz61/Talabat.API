using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace Talabat.APIs.Extensions
{
    public static class SwaggerServicesExtension
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Talabat API",
                    Version = "v1"
                });

                // This helps Swagger handle reference loops
                options.CustomSchemaIds(type => type.FullName);
            });

            return services;
        }

        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}