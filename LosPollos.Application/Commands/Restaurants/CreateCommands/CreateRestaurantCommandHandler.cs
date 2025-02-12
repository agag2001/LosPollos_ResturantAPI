using AutoMapper;
using LosPollos.Application.DTOs;

using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Restaurants.CreateCommands
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommnad, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IMapper _mapper;
        public CreateRestaurantCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateRestaurantCommnad request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Create new {@Restaurant}",request);
            var restaurant = _mapper.Map<Resturant>(request);
            var newRestaurant = await _unitOfWork.restaurantRepository.CreateAsync(restaurant);
            await _unitOfWork.Save();
            return newRestaurant.Id;

        }
    }
}
