

using FluentAssertions;
using LosPollos.Application.User;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LosPollos.Infrastructrue.Authrization.Requirements.Tests
{
    public class MinimumOwnerRequirementHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserHasMultiCreatedRestaurant_ShouldSuccessAuthorize()
        {
            // Arranage
       
            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);


            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var restaurants = new List<Resturant>()
            {
                new()
                {
                    OwnerId =  currentUser.id
                },
                new()
                {
                    OwnerId =  currentUser.id
                },new()
                {
                    OwnerId =  "2"
                }
            };
            unitOfWorkMock.Setup(x => x.restaurantRepository.GetAllAsync(null)).ReturnsAsync(restaurants);

            var requirements = new MinimumOwnerRequirement(2);
            var requirmentHandler = new MinimumOwnerRequirementHandler( userContextMock.Object, unitOfWorkMock.Object);
            var context = new AuthorizationHandlerContext([requirements], null, null);

            //Act
            await requirmentHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeTrue();      
        }

        [Fact()]
        public async Task HandleRequirementAsync_UserNotHaveMultiCreatedRestaurant_ShouldFail()
        {
            // Arranage

            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);


            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var restaurants = new List<Resturant>()
            {
                new()
                {
                    OwnerId =  currentUser.id
                },
               new()
                {
                    OwnerId =  "2"
                }
            };
            unitOfWorkMock.Setup(x => x.restaurantRepository.GetAllAsync(null)).ReturnsAsync(restaurants);

            var requirements = new MinimumOwnerRequirement(2);
            var requirmentHandler = new MinimumOwnerRequirementHandler(userContextMock.Object, unitOfWorkMock.Object);
            var context = new AuthorizationHandlerContext([requirements], null, null);

            //Act
            await requirmentHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeFalse();
            context.HasFailed.Should().BeTrue();
        }
    }
}