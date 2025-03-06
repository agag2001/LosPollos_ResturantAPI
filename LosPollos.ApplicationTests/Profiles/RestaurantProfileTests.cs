using Xunit;
using LosPollos.Application.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LosPollos.Domain.Entities;
using LosPollos.Application.DTOs;
using FluentAssertions;
using LosPollos.Application.Commands.Restaurants.CreateCommands;
using LosPollos.Application.Commands.Restaurants.UpdateCommands;

namespace LosPollos.Application.Profiles.Tests
{
    public class RestaurantProfileTests
    {
        private readonly IMapper _mapper;
        public RestaurantProfileTests()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<RestaurantProfile>();
            });
             _mapper = configuration.CreateMapper();
        }
        [Fact()]
        public void CreateMap_forRestaurantToRestaurantDto_MapsCorrectly()
        {
           
            //Arrange
            var resturant = new Resturant()
            {
                Name = "Los Pollos Hermanos",
                Description = "A famous fried chicken restaurant.",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "contact@lospollos.com",
                ContactNumber = "+1-555-1234",
                Address = new Address
                {
                    City = "Albuquerque",
                    Street = "524 Juan Tabo Blvd",
                    PostalCode = "87123"
                }
            };

            //Act
            var restaurantDto = _mapper.Map<RestaurantDTO>(resturant);

            //Assert
            restaurantDto.Should().NotBeNull();
            restaurantDto.Name.Should().Be(resturant.Name);
            restaurantDto.Description.Should().Be(resturant.Description);
            restaurantDto.Category.Should().Be(resturant.Category);
            restaurantDto.HasDelivery.Should().Be(resturant.HasDelivery);
            restaurantDto.City.Should().Be(resturant.Address.City);
            restaurantDto.Street.Should().Be(resturant.Address.Street);
            restaurantDto.PostalCode.Should().Be(resturant.Address.PostalCode);

        }
        [Fact]

        public void CreateMap_ForCreateRestaurantCommandToRestaurant_ShouldMapsCorrectly()
        {
            var command = new CreateRestaurantCommnad()
            {
                Name = "Los Pollos Hermanos",
                Description = "A famous fried chicken restaurant known for its delicious fried chicken.",
                Category = "Restaurant",
                HasDelivery = true,
                ContactEmail = "contact@lospollos.com",
                ContactNumber = "+1-555-1234", 
                City = "Albuquerque",
                Street = "524 Juan Tabo Blvd",
                PostalCode = "87123"
            };
            //Act
            var restaurant = _mapper.Map<Resturant>(command);



            //Assert
            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
            restaurant.ContactEmail.Should().Be(command.ContactEmail);
            restaurant.ContactNumber.Should().Be(command.ContactNumber);
            restaurant.Address.City.Should().Be(command.City);
            restaurant.Address.Street.Should().Be(command.Street);
            restaurant.Address.PostalCode.Should().Be(command.PostalCode);


        }


        [Fact]
        public void CreateMap_ForUpdateRestaurantCommandToRestaurant_ShouldMapsCorrectly()
        {
            var command = new UpdateRestaurantCommand()
            {
                Id = 1, 
                Name = "Los Pollos Hermanos",
                Description = "A famous fried chicken restaurant known for its delicious fried chicken.",
                HasDelivery = true,
          
            };
            //Act
            var restaurant = _mapper.Map<Resturant>(command);



            //Assert
            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(command.Id);      
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
            

        }
    }
}