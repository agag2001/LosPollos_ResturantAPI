using FluentValidation;
using FluentValidation.AspNetCore;
using LosPollos.Application.Services.Implementation;
using LosPollos.Application.Services.Interfaces;
using LosPollos.Application.Specefications;
using LosPollos.Application.User;
using Microsoft.Extensions.DependencyInjection;
namespace LosPollos.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;


            services.AddMediatR(cofig => cofig.RegisterServicesFromAssembly(assembly));
           
            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly).
                AddFluentValidationAutoValidation();
            services.AddScoped<IJwtServices, JwtServices>();
            services.AddScoped<IEmailServices, EmailServices>();  
            services.AddHttpContextAccessor();      
            services.AddScoped<IUserContext,UserContext>();
       
        }
    }
}
