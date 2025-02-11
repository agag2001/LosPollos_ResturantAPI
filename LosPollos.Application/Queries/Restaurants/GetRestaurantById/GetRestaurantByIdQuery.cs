using LosPollos.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Queries.Restaurants.GetRestaurantById
{
    public  class GetRestaurantByIdQuery : IRequest<RestaurantDTO?>
    {
        public GetRestaurantByIdQuery(int Id)
        {
            this.Id = Id;       
            
        }
        public int Id { get; }   
    }
}
