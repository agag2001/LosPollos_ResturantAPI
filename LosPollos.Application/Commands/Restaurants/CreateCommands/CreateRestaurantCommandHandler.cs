using AutoMapper;
using LosPollos.Application.DTOs;
using LosPollos.Application.User;
using LosPollos.Domain.Constant;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using LosPollos.Domain.Interfaces;
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
        private readonly IUserContext _usercontext;
        private readonly IRestaurantAuhtorizationServices _AuthServices;



        public CreateRestaurantCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IUserContext usercontext, IRestaurantAuhtorizationServices authServices)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _usercontext = usercontext;
            _AuthServices = authServices;
        }
        public async Task<int> Handle(CreateRestaurantCommnad request, CancellationToken cancellationToken)
        {
            var currentUser =  _usercontext.GetCurrentUser();   

            _logger.LogInformation("user: {email} {userId }Create new {@Restaurant}",currentUser!.email,currentUser.id,request);

           
            var restaurant = _mapper.Map<Resturant>(request);
            if (!_AuthServices.Authorize(restaurant, ResourceOperation.Create))
                throw new ForbidException();
            restaurant.OwnerId = currentUser.id; 
            var newRestaurant = await _unitOfWork.restaurantRepository.CreateAsync(restaurant);
            await _unitOfWork.Save();
            return newRestaurant.Id;

        }
    }
}
