using LosPollos.Application.DTOs;
using LosPollos.Application.Services.Interfaces;
using LosPollos.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Services.Implementation
{
    public  class JwtServices:IJwtServices
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public JwtServices(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<AuthResponseDTO> GenerateToken(AppUser user)
        {

            // 2) payload
            List<Claim> userClaims = new List<Claim>();

            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            userClaims.Add(new Claim(ClaimTypes.Name, user.FullName));
            userClaims.Add(new Claim(ClaimTypes.Email, user.Email));

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));

            }

            //3) signature
            SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT")["SecretKey"]));

            SigningCredentials credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],  
                expires: DateTime.UtcNow.AddHours(12),
                claims: userClaims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])),
                    SecurityAlgorithms.HmacSha256)
            );


            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpairedAt = DateTime.Now.AddHours(12),

            };
        }
    }
}
