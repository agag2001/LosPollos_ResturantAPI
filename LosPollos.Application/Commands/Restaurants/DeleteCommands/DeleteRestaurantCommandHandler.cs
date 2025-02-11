using AutoMapper;
using LosPollos.Application.Commands.Restaurants.CreateCommands;
using LosPollos.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LosPollos.Application.Commands.Restaurants.DeleteCommands
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
       
        public DeleteRestaurantCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteRestaurantCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"delete restaurant With id: {request.Id}");
            var restaurant = await _unitOfWork.restaurantRepository.GetAsync(x => x.Id == request.Id);
            if (restaurant is null)
                return false;

            await _unitOfWork.restaurantRepository.DeleteAsync(restaurant);
            await _unitOfWork.Save();
            return true;
           
        }
    }
}
