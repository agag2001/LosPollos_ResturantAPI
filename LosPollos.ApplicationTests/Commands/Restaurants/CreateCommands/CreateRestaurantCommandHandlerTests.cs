using Xunit;
using Moq;
using LosPollos.Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using AutoMapper;
using LosPollos.Application.User;
using LosPollos.Domain.Interfaces;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Constant;
using FluentAssertions;

namespace LosPollos.Application.Commands.Restaurants.CreateCommands.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCreateCommand_ShouldReturnRestaurantId()
        {

            //Arrange
            var command = new CreateRestaurantCommnad();
            var restaurant = new Resturant();
            var newRestaurant = new Resturant()
            {
                Id = 1
            };
           


            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x =>  x.restaurantRepository.CreateAsync(restaurant)).ReturnsAsync(newRestaurant);
            unitOfWorkMock.Setup(x => x.Save()).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

            var mapperMock = new Mock<IMapper>();
            
            mapperMock.Setup(x => x.Map<Resturant>(command)).Returns(restaurant);


            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("Owner-id", "test@test.com", [UserRoles.Owner], null, null);
            userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);

            var authorizationRestaurantMock = new Mock<IRestaurantAuhtorizationServices>();
            authorizationRestaurantMock.Setup(x => x.Authorize(restaurant, ResourceOperation.Create)).Returns(true);

            var createRestaurantHandler = new CreateRestaurantCommandHandler(unitOfWorkMock.Object, loggerMock.Object, mapperMock.Object, 
                userContextMock.Object, authorizationRestaurantMock.Object);

            //Act
            var result = await createRestaurantHandler.Handle(command, CancellationToken.None);


            //Assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("Owner-id");

            // make sure that the invocation of the save and the create restaurnt Made only One tim
            unitOfWorkMock.Verify(x => x.restaurantRepository.CreateAsync(restaurant), Times.Once);
            unitOfWorkMock.Verify(x => x.Save(), Times.Once);


        }
    }
}