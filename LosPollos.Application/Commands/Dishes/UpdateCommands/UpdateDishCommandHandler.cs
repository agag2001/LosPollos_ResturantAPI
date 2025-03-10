using AutoMapper;
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

namespace LosPollos.Application.Commands.Dishes.UpdateCommands
{
    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand>
    {
        private readonly ILogger<UpdateDishCommandHandler> _logger;     
        private readonly IUnitOfWork     _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDishCommandHandler(ILogger<UpdateDishCommandHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Dish with Id: {dishId} in restuarnt{restarantId}", request.DishId, request.RestaurantId);

            var restaurant = await _unitOfWork.restaurantRepository.GetAsync(x=>x.Id == request.RestaurantId,"Dishes");        
            if (restaurant == null) 
                throw new  NotFoundException(nameof(Resturant),request.RestaurantId.ToString());       

            var dish = restaurant.Dishes.FirstOrDefault(x=>x.Id == request.DishId);
            if (dish == null)
                throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            // mapping
            dish.Description = request.Description;
            dish.Price = request.Price;
            dish.KiloCalories = request.KiloCalories;
           await _unitOfWork.dishRepository.UpdateAsync(dish);
            await _unitOfWork.Save();
        }
    }
}
