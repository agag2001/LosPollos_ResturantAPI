﻿using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Account.ResetPassword
{
    public class ResetPasswordCommand:IRequest
    {
        public string? Email {  get; set; }      
        public string ?Token {  get; set; }
        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
        [Compare("Password")]       
       public string PasswordConfirm {  get; set; }     

    }
}
