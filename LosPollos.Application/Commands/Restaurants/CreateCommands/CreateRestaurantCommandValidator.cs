
using FluentValidation;
using LosPollos.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace LosPollos.Application.Commands.Restaurants.CreateCommands
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommnad>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.CantactEmail).EmailAddress().WithMessage("invalid Email Address");

            RuleFor(dto => dto.Name).Length(3, 100).WithMessage("the Length must be between 3-100 chracter");

            RuleFor(dto => dto.PostalCode).Matches(@"^\d{5}$").WithMessage("the Post code should be 5 digits . XXXXX ");


        }
    }
}
 