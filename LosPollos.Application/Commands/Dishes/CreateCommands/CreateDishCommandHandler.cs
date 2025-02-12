using AutoMapper;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using LosPollos.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Dishes.CreateCommands
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand,int>
    {
        private readonly IUnitOfWork _unitOfWork;   
        private readonly ILogger<CreateDishCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateDishCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateDishCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new Dish{@dish} ", request);
            var restaurant = await _unitOfWork.restaurantRepository.GetAsync(x => x.Id == request.ResturantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(Resturant), request.ResturantId.ToString());
            var dish = _mapper.Map<Dish>(request);
            var CreatedDish= await _unitOfWork.dishRepository.CreateAsync(dish);
            await _unitOfWork.Save();
            return CreatedDish.Id;
           
        }
    }
}
