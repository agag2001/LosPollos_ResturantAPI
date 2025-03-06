using Xunit;
using LosPollos.Application.Commands.Restaurants.CreateCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace LosPollos.Application.Commands.Restaurants.CreateCommands.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotReturnValidationErrors()
        {
            //Arrange
            var command = new CreateRestaurantCommnad()
            {
                Name = "test",
                Category = "Egyptian",
                PostalCode = "12345",
                ContactEmail = "test@test.com"
            };
            var validator = new CreateRestaurantCommandValidator();
            //Act
            
            var result = validator.TestValidate(command);
            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForInvliadCommand_ShouldReturnValidationErrors()
        {
            //Arrange
            var command = new CreateRestaurantCommnad()
            {
                Name = "tt",
                Category = "brazilian",
                PostalCode = "123-45",
                ContactEmail = "testtest.com"
            };
            var validator = new CreateRestaurantCommandValidator();
            //Act

            var result = validator.TestValidate(command);
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Category);
        }
        [Theory]
        [InlineData("Italian")]
        [InlineData("Mexician")]
        [InlineData("Egyptian")]
        [InlineData("American")]
        [InlineData("Palistainian")]

        public void Validator_ForValidCateigory_ShouldNotReturnValidationErrorsForCategory(string cateigory)
        {
            var command = new CreateRestaurantCommnad()
            {
                Category = cateigory
            };
            var validator = new CreateRestaurantCommandValidator();

            //Act
            var result = validator.TestValidate(command);   
            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Category);        

        }
        [Theory]
        [InlineData("23123222")]
        [InlineData("11-11")]
        [InlineData("2321")]
        [InlineData("222")]
        [InlineData("155555")]
        public void Validator_ForInvalidPostalCode_ShouldReturnValidationErrorsForPostalCode(string postalCode)
        {
            var command = new CreateRestaurantCommnad()
            {
                PostalCode = postalCode
            };
            var validator = new CreateRestaurantCommandValidator();

            //Act
            var result = validator.TestValidate(command);
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.PostalCode);

        }
      
    }
}