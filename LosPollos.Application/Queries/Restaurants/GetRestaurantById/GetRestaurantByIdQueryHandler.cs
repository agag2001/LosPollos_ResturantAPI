using AutoMapper;
using LosPollos.Application.DTOs;
using LosPollos.Application.Queries.Restaurants.GetAllRestaurants;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using LosPollos.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Queries.Restaurants.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDTO>
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetRestaurantByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        public GetRestaurantByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<RestaurantDTO> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get  Restaurant with {@request.Id}",request.Id);
            var resutaurant = await _unitOfWork.restaurantRepository.GetAsync(x => x.Id == request.Id, "Dishes")
                ?? throw new NotFoundException(nameof(Resturant), request.Id.ToString()); ;
            var restaurantDTO = _mapper.Map<RestaurantDTO>(resutaurant);
            return restaurantDTO;
        }
    }
}
