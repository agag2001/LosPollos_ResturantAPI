using LosPollos.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Queries.Dishes.GetAllDishes
{
    public class GetAllDishesQuery:IRequest<IEnumerable<DishDTO>>
    {
        public GetAllDishesQuery(int restaurantId)
        {
            RestaurantId = restaurantId;        
        }
        public int RestaurantId {  get; private set; }
    }
}
