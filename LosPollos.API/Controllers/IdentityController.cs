using LosPollos.Application.Commands.Identity.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LosPollos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return NoContent(); 
        }
    }
}
