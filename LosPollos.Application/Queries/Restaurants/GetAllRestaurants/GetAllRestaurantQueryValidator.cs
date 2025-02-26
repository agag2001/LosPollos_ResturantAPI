using FluentValidation;
using LosPollos.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Queries.Restaurants.GetAllRestaurants
{
    public class GetAllRestaurantQueryValidator:AbstractValidator<GetAllRestaurantsQuery>
    {
        private string[] AllawoedSortColumns = [nameof(RestaurantDTO.Name),
            nameof(RestaurantDTO.Description),nameof(RestaurantDTO.Category)];
        public GetAllRestaurantQueryValidator()
        {
            RuleFor(x=>x.PageNumber).GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber must be positive value");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 20)
                .WithMessage("Page Size must be posivte and less than 20");

            RuleFor(x=>x.SortBy).Must(x=>AllawoedSortColumns.Contains(x))
                .When(x=>x.SortBy !=null)
                .WithMessage($"you can sort with {string.Join(", ",AllawoedSortColumns)} only ");      
        }
    }
}
