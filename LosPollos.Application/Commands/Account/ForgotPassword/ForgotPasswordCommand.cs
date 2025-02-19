using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Account.ForgotPassword
{
    public class ForgotPasswordCommand:IRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

    }
}
