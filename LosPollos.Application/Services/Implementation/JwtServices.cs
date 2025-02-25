using LosPollos.Application.DTOs;
using LosPollos.Application.Services.Interfaces;
using LosPollos.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using LosPollos.Infrastructrue.Authorization;
using System.Security.Claims;
using System.Text;


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

            if (user.BirthDate.HasValue)
                userClaims.Add(new Claim(AppClaimTypes.BirthDate, user.BirthDate.Value.ToString("yyyy-MM-dd")));

            if (user.Nationality != null)
                userClaims.Add(new Claim(AppClaimTypes.Nationality, user.Nationality));

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
