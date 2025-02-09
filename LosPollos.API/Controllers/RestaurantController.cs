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
    }
}
