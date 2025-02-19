﻿using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces.Repository;
using LosPollos.Infrastructrue.Data;
using LosPollos.Infrastructrue.Repository;
using LosPollos.Infrastructrue.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LosPollos.Infrastructrue.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("constr");
            services.AddDbContext<AppDbContext>(options =>
            {
                // used to add info about the prameter of the query Ex ... where id  = @p1(22)
                options.UseSqlServer(connectionString).EnableSensitiveDataLogging();
            });
            services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();      
            services.AddScoped<IResturantSeeder,ResturantSeeder>(); 
            services.AddScoped<IUnitOfWork,UnitOfWork>();

            // jwt Configuration
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidIssuer = configuration["JWT:Issuer"] ,   
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                };

            });

        }
    }
}