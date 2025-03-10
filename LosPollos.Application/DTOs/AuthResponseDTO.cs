using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LosPollos.Application.DTOs
{
    public  class AuthResponseDTO
    {
        public string Token { get; set; }    
/*        public DateTime ExpairedAt { get; set; }*/

        public string? RefreshToken { get; set; }        
        public DateTime RefreshTokenExpiration { get; set; }
       
    }
}
