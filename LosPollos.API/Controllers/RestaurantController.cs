using LosPollos.Application.Commands.Restaurants.CreateCommands;
using LosPollos.Application.Queries.Restaurants.GetAllRestaurants;
using LosPollos.Application.Queries.Restaurants.GetRestaurantById;
using LosPollos.Application.Commands.Restaurants.DeleteCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using LosPollos.Application.Commands.Restaurants.UpdateCommands;
using LosPollos.Application.DTOs;
using LosPollos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LosPollos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
       private readonly IMediator _mediator;    
        public RestaurantController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAllRestaurants()
        {
            Thread.Sleep(4000);
            var restaurants = await _mediator.Send(new GetAllRestaurantsQuery());         
            return Ok(restaurants);
    }
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDTO>> GetById([FromRoute] int id)
        {
            // the handling of not found restarunt is done by the custom exception(notFoundException)
            // so if there is a restaurant with a spacific Id it should return to the User
            // if not the NotFoundException is thrown in the middelware
            var restaurant  = await _mediator.Send(new GetRestaurantByIdQuery(id));       
                
            return Ok(restaurant);      
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommnad commmad)
        {
            var id = await _mediator.Send(commmad);
            return CreatedAtAction(nameof(GetById), new { id },null);   

        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]    // tell the swagger the expected return type  
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            // the handling of not found restarunt is done by the custom exception(notFoundException)
            // so if there is a restaurant with a spacific Id it should return to the User

            await _mediator.Send( new DeleteRestaurantCommand(id));       
             return NoContent();
          
        }


        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant(UpdateRestaurantCommand command,[FromRoute] int id)
        {

            command.Id = id;        
            await _mediator.Send(command);         
            return NoContent(); 
            
        }

    }
}
