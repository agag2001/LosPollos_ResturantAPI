using FluentAssertions;
using LosPollos.Domain.Constant;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace LosPollos.Application.User.Tests
{
    public class CurrentUserTests
    {
        // Naming Convention >>> function_Scenario_ReturnType
        [Theory]
        [InlineData(UserRoles.User)]
        [InlineData(UserRoles.Admin)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            //AAA
            // Arrange

            var user = new CurrentUser("1", "user@agag.com", [UserRoles.Admin, UserRoles.User], null, null);


            //Act
            var result = user.IsInRole(roleName);

            //Assert
            result.Should().BeTrue();       

        }

        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            //AAA
            // Arrange

            var user = new CurrentUser("1", "user@agag.com", [UserRoles.Admin, UserRoles.User], null, null);


            //Act
            var result = user.IsInRole(UserRoles.Owner);

            //Assert
            result.Should().BeFalse();

        }


        [Fact()]
        public void IsInRole_WithDifferentRoleCase_ShouldReturnFalse()
        {
            //AAA
            // Arrange

            var user = new CurrentUser("1", "user@agag.com", [UserRoles.Admin, UserRoles.User], null, null);


            //Act
            var result = user.IsInRole(UserRoles.Admin.ToLower());

            //Assert
            result.Should().BeFalse();

        }
    }
}