using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Dishes.CreateCommands
{
    public class CreateDishCommandValidator:AbstractValidator<CreateDishCommand>        
    {
        public CreateDishCommandValidator()
        {
            RuleFor(x=>x.Price).GreaterThanOrEqualTo(0).WithMessage("the price Must be Non-Negative value");
            RuleFor(x=>x.KiloCalories).GreaterThanOrEqualTo(0).WithMessage("the Kilo Calories Must be Non-Negative value");
        }
    }
}
