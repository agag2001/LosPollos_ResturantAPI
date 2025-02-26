using LosPollos.Application.Common;
using LosPollos.Application.DTOs;
using LosPollos.Domain.Constant;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Queries.Restaurants.GetAllRestaurants
{
    public  class GetAllRestaurantsQuery:IRequest<PagedResults<RestaurantDTO>>
    {
            
        public string? SearchPhrase {  get; set; }       
        public int PageSize {  get; set; }      
        public int PageNumber {  get; set; }        
        public SortDirection SortDirection { get; set; }        
        public string? SortBy {  get; set; }        
    }
}
