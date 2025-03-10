using LosPollos.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Account.RefreshToken
{
    public class RefreshTokenCommand:IRequest<AuthResponseDTO>
    {
        public RefreshTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;        
        }
        public string RefreshToken { get; set; }
    }
}
