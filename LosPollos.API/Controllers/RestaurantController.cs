using LosPollos.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LosPollos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IResturantServices _resturantServices;
        public RestaurantController(IResturantServices resturantServices)
        {
            _resturantServices = resturantServices;
        }
        [HttpGet]       
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants =  await _resturantServices.GetAllRestaurants();       
            return Ok(restaurants);
        }
        [HttpGet("{id}")]
        public async Task  <IActionResult> GetById([FromRoute] int id)
        {
            var restaurant  = await _resturantServices.GetById(id);       
            if (restaurant == null) 
                return NotFound();      
            return Ok(restaurant);      
        }
    }
}
