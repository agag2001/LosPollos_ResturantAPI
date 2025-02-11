using AutoMapper;
using LosPollos.Application.Commands.Restaurants.CreateCommands;
using LosPollos.Domain.Entities;
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
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
        private readonly IMapper _mapper;
        public UpdateRestaurantCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurantFromDB= await _unitOfWork.restaurantRepository.GetAsync(x=>x.Id==request.Id);
            if (restaurantFromDB == null)
                return false;
            _mapper.Map(request,restaurantFromDB);
   
            await _unitOfWork.Save();
            return true;        
            
            
        }
    }
}
