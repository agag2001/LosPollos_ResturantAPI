using LosPollos.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Account.Login
{
    public class LoginCommand:IRequest<AuthResponseDTO>
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]       
        public string Password { get; set; }        
    }
}
