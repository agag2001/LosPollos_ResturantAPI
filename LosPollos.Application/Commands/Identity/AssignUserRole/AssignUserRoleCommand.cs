﻿using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Identity.AssignUserRole
{
    public class AssignUserRoleCommand:IRequest
    {
        [EmailAddress]
        public string Email { get; set; } 
        public string RoleName { get; set; }        
    }
}
