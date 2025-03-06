using Xunit;

using Moq;
using LosPollos.Domain.Interfaces.Repository;

using AutoMapper;
using LosPollos.Domain.Interfaces;
using LosPollos.Domain.Entities;

using LosPollos.Domain.Constant;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using FluentAssertions;
using LosPollos.Application.Commands.Account.Register;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using LosPollos.Domain.Exceptions;

namespace LosPollos.Application.Commands.Restaurants.UpdateCommands.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidUpdateRestaurantCommand_ShouldNotReturnAnyErrors()
        {
            //Arrange

            var restaurantFromDb = new Resturant()
            {
                Id= 1,
                Name="OldName"
            };
            var commad = new UpdateRestaurantCommand()
            {
                Id = 1,
                Name = "Updated Name",
                Description = "Updated Description",
                HasDelivery = true
            };
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.restaurantRepository
            .GetAsync( It.IsAny<Expression<Func<Resturant,bool>>>(),null)).ReturnsAsync(restaurantFromDb); 
            unitOfWorkMock.Setup(x=>x.Save()).Returns(Task.CompletedTask);      

            var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map(commad, restaurantFromDb)).Returns(restaurantFromDb);

            var restaurantAuthenicated =  new Mock<IRestaurantAuhtorizationServices>();
            restaurantAuthenicated.Setup(x=>x.Authorize(restaurantFromDb, ResourceOperation.Update)).Returns(true);            


            var Handler = new UpdateRestaurantCommandHandler(unitOfWorkMock.Object,loggerMock.Object,mapperMock.Object,restaurantAuthenicated.Object);
            //Act

           await  Handler. Handle(commad,CancellationToken.None);


            //Assert

            unitOfWorkMock.Verify(x => x.Save(), Times.Once);
            unitOfWorkMock.Verify(x => x.restaurantRepository.GetAsync(It.IsAny<Expression<Func<Resturant, bool>>>(), null), Times.Once);
            mapperMock.Verify(x => x.Map(commad, restaurantFromDb), Times.Once);
        }
        [Fact]
        public async Task Handle_ForNotFoundRestaurant_ShouldThrowNotFoundException()
        {

            var restaurantFromDb = new Resturant()
            {
                Id = 1,
                Name = "OldName"
            };
            var commad = new UpdateRestaurantCommand()
            {
                Id = 2,
                Name = "Updated Name",
                Description = "Updated Description",
                HasDelivery = true
            };
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.restaurantRepository
            .GetAsync(It.IsAny<Expression<Func<Resturant, bool>>>(), null)).ReturnsAsync((Resturant)null);
            unitOfWorkMock.Setup(x => x.Save()).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map(commad, restaurantFromDb)).Returns(restaurantFromDb);

            var restaurantAuthenicated = new Mock<IRestaurantAuhtorizationServices>();
            restaurantAuthenicated.Setup(x => x.Authorize(restaurantFromDb, ResourceOperation.Update)).Returns(true);


            var Handler = new UpdateRestaurantCommandHandler(unitOfWorkMock.Object, loggerMock.Object, mapperMock.Object, restaurantAuthenicated.Object);
            //Act

            Func<Task> result =  async()=>await Handler.Handle(commad, CancellationToken.None);
            //Assert
            await result.Should().ThrowAsync<NotFoundException>();
            unitOfWorkMock.Verify(x=>x.Save(),Times.Never());       
            unitOfWorkMock.Verify(x=>x.restaurantRepository.GetAsync(It.IsAny<Expression<Func<Resturant, bool>>>(), null),Times.Once());


        }

        [Fact]
        public async Task Handle_ForUnAuthorizeUser_ShouldThrowForbidException()
        {

            var restaurantFromDb = new Resturant()
            {
                Id = 1,
                Name = "OldName"
            };
            var commad = new UpdateRestaurantCommand()
            {
                Id = 2,
                Name = "Updated Name",
                Description = "Updated Description",
                HasDelivery = true
            };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.restaurantRepository
            .GetAsync(It.IsAny<Expression<Func<Resturant, bool>>>(), null)).ReturnsAsync(restaurantFromDb);
            unitOfWorkMock.Setup(x => x.Save()).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map(commad, restaurantFromDb)).Returns(restaurantFromDb);

            var restaurantAuthenicated = new Mock<IRestaurantAuhtorizationServices>();
            restaurantAuthenicated.Setup(x => x.Authorize(restaurantFromDb, ResourceOperation.Update)).Returns(false);


            var Handler = new UpdateRestaurantCommandHandler(unitOfWorkMock.Object, loggerMock.Object, mapperMock.Object, restaurantAuthenicated.Object);
            //Act

            Func<Task> result = async () => await Handler.Handle(commad, CancellationToken.None);
            //Assert
            await result.Should().ThrowAsync<ForbidException>();
            unitOfWorkMock.Verify(x => x.Save(), Times.Never());
            unitOfWorkMock.Verify(x => x.restaurantRepository.GetAsync(It.IsAny<Expression<Func<Resturant, bool>>>(), null), Times.Once());


        }
    }
}