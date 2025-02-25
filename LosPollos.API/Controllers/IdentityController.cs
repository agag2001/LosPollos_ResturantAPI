using LosPollos.Application.Commands.Identity.AssignUserRole;
using LosPollos.Application.Commands.Identity.DeleteUserRole;
using LosPollos.Application.Commands.Identity.UpdateUser;
using LosPollos.Domain.Constant;
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

        [HttpPost("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteUserRole(DeleteUserRoleCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
