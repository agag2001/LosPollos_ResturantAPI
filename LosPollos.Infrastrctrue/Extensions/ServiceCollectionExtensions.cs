using LosPollos.Domain.Interfaces.Repository;
using LosPollos.Infrastructrue.Data;
using LosPollos.Infrastructrue.Repository;
using LosPollos.Infrastructrue.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LosPollos.Infrastructrue.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("constr");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IResturantSeeder,ResturantSeeder>(); 
            services.AddScoped<IUnitOfWork,UnitOfWork>();     
               


        }
    }
}