using AutoMapper;
using LosPollos.Application.Commands.Restaurants.CreateCommands;
using LosPollos.Domain.Constant;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using LosPollos.Domain.Interfaces;
using LosPollos.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LosPollos.Application.Commands.Restaurants.DeleteCommands
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
        private readonly IRestaurantAuhtorizationServices _AuthServices;

        public DeleteRestaurantCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantAuhtorizationServices authServices)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _AuthServices = authServices;
        }

        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("delete restaurant With id: {@request.Id}",request.Id);
            var restaurant = await _unitOfWork.restaurantRepository.GetAsync(x => x.Id == request.Id);
            if (restaurant is null)
                throw new NotFoundException(nameof(Resturant), request.Id.ToString());
            if (!_AuthServices.Authorize(restaurant, ResourceOperation.Delete))
                throw new ForbidException();
            await _unitOfWork.restaurantRepository.DeleteAsync(restaurant);
            await _unitOfWork.Save();
          
           
        }
    }
}
