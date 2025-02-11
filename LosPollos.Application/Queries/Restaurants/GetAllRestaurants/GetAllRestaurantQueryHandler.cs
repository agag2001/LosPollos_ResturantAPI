using AutoMapper;
using LosPollos.Application.Commands.Restaurants.CreateCommands;
using LosPollos.Application.DTOs;
using LosPollos.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Queries.Restaurants.GetAllRestaurants
{
    public class GetAllRestaurantQueryHandler : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDTO>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllRestaurantQueryHandler> _logger;
        private readonly IMapper _mapper;
        public GetAllRestaurantQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllRestaurantQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Get All Restaurants");
            var restaurants = await _unitOfWork.restaurantRepository.GetAllAsync();
            // mapping restaurants to DTO
            var restaurantDTOs = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            return restaurantDTOs;

        }
    }
}
