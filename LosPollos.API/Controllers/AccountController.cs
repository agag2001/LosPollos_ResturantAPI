using LosPollos.Application.Commands.Account.ForgotPassword;
using LosPollos.Application.Commands.Account.Login;
using LosPollos.Application.Commands.Account.RefreshToken;
using LosPollos.Application.Commands.Account.Register;
using LosPollos.Application.Commands.Account.ResetPassword;
using LosPollos.Application.Commands.Account.RevokeToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LosPollos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            await _mediator.Send(command);

            return Ok("User Created Successfully !");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var token= await _mediator.Send(command);
            return Ok(token);   
        }
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand commnad)
        {
            await _mediator.Send(commnad);
            return Ok("Check Your email");
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand commnad)
        {
            await _mediator.Send(commnad);
            return Ok();
        }

        [HttpPost("refreshToken")]      
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var authResponse = await _mediator.Send(command);        
            return Ok(authResponse);      
        }
        [HttpPost("revokeToken")]       
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenCommand command)
        {
            await _mediator.Send(command);
            return Ok("Token revoked");
        }   

    }
}
