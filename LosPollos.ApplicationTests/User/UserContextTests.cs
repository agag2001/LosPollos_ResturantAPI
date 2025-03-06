using Xunit;
using LosPollos.Application.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using LosPollos.Domain.Constant;
using FluentAssertions;

namespace LosPollos.Application.User.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldRetrunCurrentUser()
        {
            //Arranage
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var birthDate = new DateOnly(2011, 1, 1);
            var claims = new List<Claim>()
            {
                new (ClaimTypes.NameIdentifier,"1"),
                new (ClaimTypes.Email,"test@test.com"),
                new (ClaimTypes.Role,UserRoles.Admin),
                new (ClaimTypes.Role,UserRoles.User),
                new ("Nationality","Egyptain"),
                new("BirthDate",birthDate.ToString("yyyy-MM-dd"))
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims,"test"));
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });
            var userContext = new UserContext(mockHttpContextAccessor.Object);
            //Act
            var result = userContext.GetCurrentUser();
            //Assert
            result.Should().NotBeNull();    
            result.id.Should().Be("1");
            result.email.Should().Be("test@test.com");
            result.roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            result.nationality.Should().Be("Egyptain");          
            result.birthDate.Should().Be(birthDate);
        }

        [Fact()]
        public void GetCurrentUser_WithNoUser_ThrowsInvalidOperationException()
        {
            //Arranage
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
           
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns((HttpContext)null!);
            var userContext = new UserContext(mockHttpContextAccessor.Object);
           
            //Act
            Action result =  ()=> userContext.GetCurrentUser();
            //Assert
            result.Should().Throw<InvalidOperationException>().WithMessage("User is not Present");
           
        }
    }
}