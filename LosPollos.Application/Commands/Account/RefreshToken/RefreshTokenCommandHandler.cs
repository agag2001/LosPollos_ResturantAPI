using LosPollos.Application.DTOs;
using LosPollos.Application.Services.Interfaces;
using LosPollos.Application.User;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Account.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDTO>
    {
        private readonly IUserContext _userContext;
        private readonly IJwtServices _jwtServices;
        private readonly UserManager<AppUser> _userManager;

        public RefreshTokenCommandHandler(IUserContext userContext, IJwtServices jwtServices, UserManager<AppUser> userManager)
        {
            _userContext = userContext;
            _jwtServices = jwtServices;
            _userManager = userManager;
        }

        public async Task<AuthResponseDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var authResponse = new AuthResponseDTO();       

          var user  = _userManager.Users.SingleOrDefault(x => x.RefreshTokens.Any(t => t.Token == request.RefreshToken));
            if (user == null)
                throw new UserException("Invalid Refresh Token");
            var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken);
            if (refreshToken == null)
                throw new NotFoundException(nameof(RefreshToken), request.RefreshToken);        

            if(!refreshToken.IsActive)
                throw new UserException("Refresh token is not active");

            refreshToken.RevokedAt = DateTime.UtcNow;   
            var newRefreshToken =   _jwtServices.GenerateRefreshToken();
            //store the new refresh token   
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user); 

            var AccessToken = await _jwtServices.CreateToken(user);

            authResponse.Token = new JwtSecurityTokenHandler().WriteToken(AccessToken);
            authResponse.RefreshToken = newRefreshToken.Token;
            authResponse.RefreshTokenExpiration = newRefreshToken.ExpiredAt;

            return authResponse;

        }
    }
}
