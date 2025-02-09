using LosPollos.Application.Services.Interfaces;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;

namespace LosPollos.Application.Services.Implementation
{
    internal  class RestaurantServices : IResturantServices

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RestaurantServices> _logger;
        public RestaurantServices(IUnitOfWork unitOfWork, ILogger<RestaurantServices> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
       
        public async Task<IEnumerable<Resturant>> GetAllRestaurants()
        {
            _logger.LogInformation("Get All Restaurants");
            var restaurants = await _unitOfWork.restaurantRepository.GetAllAsync();
            return restaurants;
        }
    }
}
