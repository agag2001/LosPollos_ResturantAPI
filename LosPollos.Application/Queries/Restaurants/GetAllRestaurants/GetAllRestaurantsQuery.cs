using LosPollos.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Queries.Restaurants.GetAllRestaurants
{
    public  class GetAllRestaurantsQuery:IRequest<IEnumerable<RestaurantDTO>>
    {
            

    }
}
