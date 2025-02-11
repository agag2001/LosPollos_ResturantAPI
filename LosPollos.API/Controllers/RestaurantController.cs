using LosPollos.Application.Commands.Restaurants.CreateCommands;
using LosPollos.Application.Queries.Restaurants.GetAllRestaurants;
using LosPollos.Application.Queries.Restaurants.GetRestaurantById;
using LosPollos.Application.Commands.Restaurants.DeleteCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using LosPollos.Application.Commands.Restaurants.UpdateCommands;

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
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants =  await _mediator.Send(new GetAllRestaurantsQuery());         
            return Ok(restaurants);
        }
        [HttpGet("{id}")]
        public async Task  <IActionResult> GetById([FromRoute] int id)
        {
            var restaurant  = await _mediator.Send(new GetRestaurantByIdQuery(id));       
            if (restaurant == null) 
                return NotFound();      
            return Ok(restaurant);      
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommnad commmad)
        {
            var id = await _mediator.Send(commmad);
            return CreatedAtAction(nameof(GetById), new { id },null);   

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            var isDeleted = await _mediator.Send( new DeleteRestaurantCommand(id));
            if (isDeleted)
                return NoContent();
            return NotFound();
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRestaurant(UpdateRestaurantCommand command,[FromRoute] int id)
        {
            command.Id = id;        
            var isUpdated  = await _mediator.Send(command);
            if (isUpdated)
            {
                return NoContent(); 
            }
            return NotFound();
        }

    }
}
