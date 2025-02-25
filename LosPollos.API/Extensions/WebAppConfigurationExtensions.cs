using LosPollos.API.Middleware;
using LosPollos.Infrastructrue.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Security.Claims;

namespace LosPollos.API.Extensions
{
    public static class WebAppConfigurationExtensions
    {
        public static void  AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            // Swagger configuration to accept bearer token in each request
            builder.Services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"

                });
                // to put the bearer in the header of every endpoint in the swagger
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            }
                        },
                        []
                    }
                });
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // adding the services of the My exception handler middleware

            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<RequestTimeLoggingMiddleware>();



            // adding serilog configuratoins
            // the cofiguration is moved to the app.setting.Delevloper cofiguration to more readable and reuasable
            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);

            });


           



        }
    }
}
