using AutoMapper;
using LosPollos.Application.DTOs;
using LosPollos.Application.Queries.Dishes.GetAllDishes;
using LosPollos.Application.Queries.Dishes.GetDishById;
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

namespace LosPollos.Application.Queries.Dishes
{
    public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, DishDTO>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetDishByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        public GetDishByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetDishByIdQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DishDTO> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Dish Of id:{Id} from Restaurant{restID}", request.DishId, request.RestaurantId);

            var restaurnat = await _unitOfWork.restaurantRepository.GetAsync(x => x.Id == request.RestaurantId, "Dishes");
            if (restaurnat == null)
                throw new NotFoundException(nameof(Resturant), request.RestaurantId.ToString());


            var dishFromDB = restaurnat.Dishes.FirstOrDefault(x=>x.Id==request.DishId);
            if (dishFromDB == null)
                throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            var DishDto = _mapper.Map<DishDTO>(dishFromDB);     

            return DishDto;
        }
    }
}
