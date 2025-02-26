using AutoMapper;
using LosPollos.Application.Commands.Restaurants.CreateCommands;
using LosPollos.Application.Common;
using LosPollos.Application.DTOs;
using LosPollos.Application.Specefications.RestaurantSepecifications;
using LosPollos.Domain.Entities;
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
    public class GetAllRestaurantQueryHandler : IRequestHandler<GetAllRestaurantsQuery, PagedResults<RestaurantDTO>>
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
        public async Task<PagedResults<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Get All Restaurants");
          
            var (restaurants,count) = await _unitOfWork.restaurantRepository.GetAllMatching(request.SearchPhrase
                ,request.PageNumber,request.PageSize,request.SortBy,request.SortDirection);

            // mapping restaurants to DTO
            var restaurantDTOs = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            var pagedResult = new PagedResults<RestaurantDTO>(restaurantDTOs, count, request.PageSize, request.PageNumber);
            return pagedResult;

        }
    }
}
