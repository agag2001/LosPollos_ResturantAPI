using FluentValidation;
using FluentValidation.AspNetCore;
using LosPollos.Application.Services.Implementation;
using LosPollos.Application.Services.Interfaces;
using LosPollos.Application.Validators;
using Microsoft.Extensions.DependencyInjection;
namespace LosPollos.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
          
            services.AddScoped<IResturantServices, RestaurantServices>();
           
            services.AddAutoMapper(typeof(CreateRestaurantDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(CreateRestaurantDtoValidator).Assembly).
                AddFluentValidationAutoValidation();

        }
    }
}
