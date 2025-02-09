using LosPollos.Application.Services.Implementation;
using LosPollos.Application.Services.Interfaces;
using LosPollos.Domain.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
          
            services.AddScoped<IResturantServices, RestaurantServices>();
           
            services.AddAutoMapper(typeof(IResturantServices).Assembly); 

        }
    }
}
