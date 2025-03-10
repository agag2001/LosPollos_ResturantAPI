using AutoMapper.Configuration.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Dishes.UpdateCommands
{
    public class UpdateDishCommand:IRequest
    {
        public UpdateDishCommand(int restaurantId, int dishId)
        {
            RestaurantId = restaurantId;    
            DishId = dishId;    
        }
        [JsonIgnore]
        public int RestaurantId { get; set; }
        [JsonIgnore]
        public int DishId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? KiloCalories { get; set; }
    }
}
