using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Account.RevokeToken
{
    public class RevokeTokenCommand:IRequest
    {
        public RevokeTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
        public string RefreshToken { get; set; }       
    }
}
