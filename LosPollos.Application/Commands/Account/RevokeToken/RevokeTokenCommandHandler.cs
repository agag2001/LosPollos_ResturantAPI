using LosPollos.Application.DTOs;
using LosPollos.Application.Services.Interfaces;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Account.RevokeToken
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtServices _jwtServices;
        public RevokeTokenCommandHandler(UserManager<AppUser> userManager, IJwtServices jwtServices)
        {
            _userManager = userManager;
            _jwtServices = jwtServices;
        }   
        public async Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {

         

            var user = _userManager.Users.SingleOrDefault(x => x.RefreshTokens.Any(t => t.Token == request.RefreshToken));
            if (user == null)
                throw new UserException("Invalid Refresh Token");
            var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken);
            if (refreshToken == null)
                throw new NotFoundException(nameof(RefreshToken), request.RefreshToken);

            if (!refreshToken.IsActive)
                throw new UserException("Refresh token is not active");

            refreshToken.RevokedAt = DateTime.UtcNow;
           
            user.RefreshTokens.RemoveAll(x => x.RevokedAt.HasValue);
            await _userManager.UpdateAsync(user);
        }
    }
}
