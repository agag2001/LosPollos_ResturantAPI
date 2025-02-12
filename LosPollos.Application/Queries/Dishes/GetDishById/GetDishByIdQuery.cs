using LosPollos.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Queries.Dishes.GetDishById
{
    public class GetDishByIdQuery:IRequest<DishDTO>
    {
        public GetDishByIdQuery(int restaurantId,int dishId)
        {
            RestaurantId = restaurantId;    
            DishId = dishId;        
            
        }
        public int RestaurantId {  get; private set; }  
        public int DishId { get; private set; } 
    }
}
