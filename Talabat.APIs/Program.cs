using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Infrastructure;
using Talabat.Infrastructure.Data;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();

            webApplicationBuilder.Services.AddSwaggerServices();

            webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            webApplicationBuilder.Services.AddApplicationServices();

            #endregion

            var app = webApplicationBuilder.Build();

            #region Apply All Pending Migrations [Update-Database] And Data Seeding
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<StoreContext>();
            //Ask CLR To Create Object From DbContext Explicitly

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync(); //Update-Database
                await StoreContextSeed.SeedAsync(_dbContext); // Data Seeding
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during applying migration");
            }
            #endregion


            #region Configure Kesterel Middelewares
            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
