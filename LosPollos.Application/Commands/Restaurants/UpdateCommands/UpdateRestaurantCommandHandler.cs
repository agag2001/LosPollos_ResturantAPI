using AutoMapper;
using LosPollos.Application.Commands.Restaurants.CreateCommands;
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

namespace LosPollos.Application.Commands.Restaurants.UpdateCommands
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantAuhtorizationServices _AuthServices;

        public UpdateRestaurantCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantAuhtorizationServices authServices)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _AuthServices = authServices;
        }
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            // the @ is used to serialize the object  so it will displayed in the console as it's  prameter not the namespace
            _logger.LogInformation("Updating Restuarant with Id : {@request.Id} to {@request}",request.Id,request);
            var restaurantFromDB= await _unitOfWork.restaurantRepository.GetAsync(x=>x.Id==request.Id);
            if (restaurantFromDB == null)
                throw new NotFoundException(nameof(Resturant), request.Id.ToString());
            if (!_AuthServices.Authorize(restaurantFromDB, ResourceOperation.Update))
                throw new ForbidException();
            _mapper.Map(request,restaurantFromDB);
   
            await _unitOfWork.Save();
                
            
            
        }
    }
}
