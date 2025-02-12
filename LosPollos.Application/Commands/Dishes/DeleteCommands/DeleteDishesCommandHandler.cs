using AutoMapper;
using LosPollos.Application.Commands.Dishes.CreateCommands;
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

namespace LosPollos.Application.Commands.Dishes.DeleteCommands
{
    public class DeleteDishesCommandHandler : IRequestHandler<DeleteDishesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteDishesCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteDishesCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteDishesCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task Handle(DeleteDishesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting Dishes of Restaurant With Id :{ID}", request.RestaurantId);
            var restaurant  = await _unitOfWork.restaurantRepository.GetAsync(x=>x.Id == request.RestaurantId,"Dishes");
            if (restaurant == null)
                throw new NotFoundException(nameof(Resturant),request.RestaurantId.ToString());
            restaurant.Dishes.Clear();
            await _unitOfWork.Save();

            
        }
    }
}
