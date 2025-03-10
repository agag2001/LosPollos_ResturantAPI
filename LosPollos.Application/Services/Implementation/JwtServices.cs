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
using System.Security.Cryptography;


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
            var authResponseDto = new AuthResponseDTO();        

          var token =   await CreateToken(user);   
            if (user.RefreshTokens.Any(x => x.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.IsActive);
                authResponseDto.RefreshToken = activeRefreshToken.Token;        
                authResponseDto.RefreshTokenExpiration = activeRefreshToken.ExpiredAt;  
            }
            else
            {
                var refreshToken  = GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                authResponseDto.RefreshToken = refreshToken.Token;
                authResponseDto.RefreshTokenExpiration = refreshToken.ExpiredAt;
                await _userManager.UpdateAsync(user);
            }
            authResponseDto.Token = new JwtSecurityTokenHandler().WriteToken(token);

            return authResponseDto; 
        }
        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            RandomNumberGenerator.Fill(randomNumber);   
            return new RefreshToken 
            { 
                Token = Convert.ToBase64String(randomNumber),
                ExpiredAt = DateTime.UtcNow.AddDays(30)        
            };
        }
        public async Task<JwtSecurityToken> CreateToken(AppUser user)
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
                expires: DateTime.UtcNow.AddMinutes(10),
                claims: userClaims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])),
                    SecurityAlgorithms.HmacSha256)
            );
            return token;
        }
    }
}
