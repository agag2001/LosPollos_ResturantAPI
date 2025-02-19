using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Identity.UpdateUser
{
    public class UpdateUserCommand:IRequest
    {
        public DateOnly? BirthDate { get; set; }     
        public string? Nationality { get; set; }
    }
}
