using FluentValidation;
using FluentValidation.AspNetCore;
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

        }
    }
}
