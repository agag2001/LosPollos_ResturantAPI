using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Account.Register
{
    public class RegisterCommadValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterCommadValidator()
        {
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password)
             .NotEmpty().WithMessage("Password is required")
             .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
             .Matches(@"^(?=.*[a-z])(?=.*\d).+$")
             .WithMessage("Password must contain at least one lowercase letter and one number.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required")
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
