using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Account.Register
{
    public class RegisterCommand:IRequest
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; } 
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
