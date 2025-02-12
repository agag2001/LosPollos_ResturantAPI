using AutoMapper;
using LosPollos.Application.DTOs;
using LosPollos.Application.Queries.Restaurants.GetAllRestaurants;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using LosPollos.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Queries.Dishes.GetAllDishes
{
    public class GetAllDishesQueryHandler : IRequestHandler<GetAllDishesQuery, IEnumerable<DishDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllDishesQueryHandler> _logger;
        private readonly IMapper _mapper;
        public GetAllDishesQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllDishesQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<IEnumerable<DishDTO>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("get Dish of Restaurant With Id :{ID}", request.RestaurantId);
            var restaurnat = await _unitOfWork.restaurantRepository.GetAsync(x=>x.Id==request.RestaurantId,"Dishes");
            if (restaurnat==null) 
                throw new NotFoundException(nameof(Resturant),request.RestaurantId.ToString());


            var dishes = _mapper.Map<IEnumerable <DishDTO>>(restaurnat.Dishes);
            return dishes;
        }
    }
}
