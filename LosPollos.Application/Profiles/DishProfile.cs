using AutoMapper;
using LosPollos.Application.Commands.Dishes.CreateCommands;
using LosPollos.Application.DTOs;
using LosPollos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Profiles
{
    public class DishProfile: Profile
    {
        public DishProfile()
        {
            CreateMap<CreateDishCommand, Dish>();
            CreateMap<Dish, DishDTO>();
        }
    }
}
