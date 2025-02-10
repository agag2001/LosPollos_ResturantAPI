using AutoMapper;
using LosPollos.Application.DTOs;
using LosPollos.Application.Services.Interfaces;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace LosPollos.Application.Services.Implementation
{
    internal class RestaurantServices : IResturantServices

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RestaurantServices> _logger;
        private readonly IMapper _mapper;
        public RestaurantServices(IUnitOfWork unitOfWork, ILogger<RestaurantServices> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> Create(CreateRestaurantDTO restaurantDTO)
        {
            _logger.LogInformation("Create new Restaurant");
            var  restaurant = _mapper.Map<Resturant>(restaurantDTO);
            var newRestaurant  =  await _unitOfWork.restaurantRepository.CreateAsync(restaurant);
            await _unitOfWork.Save();
            return newRestaurant.Id;        
        }

        public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurants()
        {
            _logger.LogInformation("Get All Restaurants");
            var restaurants = await _unitOfWork.restaurantRepository.GetAllAsync();
            // mapping restaurants to DTO
            var restaurantDTOs =  _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            return restaurantDTOs;
        }

        public async Task<RestaurantDTO> GetById(int id)
        {
            _logger.LogInformation($"Get  Restaurant with {id}");
            var resutaurant = await _unitOfWork.restaurantRepository.GetAsync(x=>x.Id == id,"Dishes");  
            var restaurantDTO = _mapper.Map<RestaurantDTO>(resutaurant);        
            return restaurantDTO; 
        }

        
    }
}
