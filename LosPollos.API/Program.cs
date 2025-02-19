
using LosPollos.API.Extensions;
using LosPollos.API.Middleware;
using LosPollos.Application.Extensions;
using LosPollos.Infrastructrue.Data;
using LosPollos.Infrastructrue.Extensions;
using LosPollos.Infrastructrue.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace LosPollos.API
{
    
    public class Program
    {
      
        public static async  Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.AddPresentation();
            // adding dbcontext services
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();

            var app = builder.Build();

            // so the exception middleware will be the first middel ware to check if there any errors 
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeLoggingMiddleware>();

            // seeding data initaly to database
            var scope =  app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IResturantSeeder>();
            await seeder.Seed();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // logging the info about the request enpoint in the console
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
