﻿using LosPollos.Application.Commands.Dishes.CreateCommands;
using LosPollos.Application.Commands.Dishes.DeleteCommands;
using LosPollos.Application.Commands.Dishes.UpdateCommands;
using LosPollos.Application.DTOs;
using LosPollos.Application.Queries.Dishes.GetAllDishes;
using LosPollos.Application.Queries.Dishes.GetDishById;
using LosPollos.Domain.Interfaces.Repository;
using LosPollos.Infrastructrue.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LosPollos.API.Controllers
{
    [Route("api/Restaurant/{RestaurantId}/Dishes")]
    [ApiController]
    [Authorize]
    public class DishesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int RestaurantId, CreateDishCommand command)
        {
            command.ResturantId = RestaurantId;
            var DishId = await _mediator.Send(command);


            return CreatedAtAction(nameof(GetDishByID), new { RestaurantId, DishId }, null);
        }

        [HttpGet]
        [Authorize(Policy = PolicyNames.CreatedAtLeast2)]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetAllDishes([FromRoute] int RestaurantId)
        {
            var dishes = await _mediator.Send(new GetAllDishesQuery(RestaurantId));
            return Ok(dishes);
        }

        [HttpGet("{DishId}")]
        public async Task<ActionResult<DishDTO>> GetDishByID([FromRoute] int DishId, [FromRoute] int RestaurantId)
        {
            var dish = await _mediator.Send(new GetDishByIdQuery(RestaurantId, DishId));
            return Ok(dish);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllDishes([FromRoute] int RestaurantId)
        {
            await _mediator.Send(new DeleteDishesCommand(RestaurantId));

            return NoContent();
        }
        [HttpPut("{DishId}")]
        public async Task<IActionResult> UpdateDish([FromRoute] int DishId, [FromRoute] int RestaurantId,[FromBody]UpdateDishCommand command)
        {
            command.RestaurantId = RestaurantId;
            command.DishId = DishId;        
            await _mediator.Send(command);
            return NoContent();
        }
      
    }

}
