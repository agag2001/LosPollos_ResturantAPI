using FluentAssertions;
using LosPollos.APITests;
using LosPollos.Application.DTOs;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using Xunit;

namespace LosPollos.API.Controllers.Tests
{
    public class RestaurantControllerTests :IClassFixture<WebApplicationFactory<Program>>
    {
  
        private readonly WebApplicationFactory<Program> _factroy;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock=new Mock<IUnitOfWork>();
        public RestaurantControllerTests(WebApplicationFactory<Program> factroy)
        {
            _factroy = factroy.WithWebHostBuilder(builder=>builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                //instead of fethcing the local database we are realy on mocking
                services.Replace(ServiceDescriptor.Scoped(typeof(IUnitOfWork), _ => _unitOfWorkMock.Object));

            }));
        }
        [Fact]
        public async void GetAllRestaurants_ForValidRequest_ShouldReturn200Ok()
        {
            // Arrange
            var client = _factroy.CreateClient();
            //Act
           var result =  await client.GetAsync("api/Restaurant?pageNumber=1&pageSize=10");
            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);    
        }

        [Fact]
        public async void GetAllRestaurants_ForInValidRequest_ShouldReturn400BadRequest()
        {
            // Arrange
            var client = _factroy.CreateClient();
            //Act
            var result = await client.GetAsync("api/Restaurant");
            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }


        [Fact]
        public async void GetRestarantById_ForNotFoundRestaurant_ShouldReturn404NotFound()
        {
            // Arrange
            var client = _factroy.CreateClient();
            var id = 222;
            _unitOfWorkMock.Setup(x=>x.restaurantRepository.GetAsync(x=>x.Id==id, "Dishes")).ReturnsAsync((Resturant?)null);       
            //Act
            var result = await client.GetAsync($"api/Restaurant/{id}");
            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetRestarantById_ForExistsRestaurant_ShouldReturn200Ok()
        {
            // Arrange
            var client = _factroy.CreateClient();
            var id = 22;
            var restaurant = new Resturant { Id = id, Name = "Test" };
            _unitOfWorkMock
                       .Setup(x => x.restaurantRepository.GetAsync(It.IsAny<Expression<Func<Resturant, bool>>>(), "Dishes"))
                       .ReturnsAsync(restaurant);

            //Act
            var result = await client.GetAsync($"api/Restaurant/{id}");
            var restaruantDto = await result.Content.ReadFromJsonAsync<RestaurantDTO>();   
            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            restaruantDto.Should().NotBeNull();     
            restaruantDto.Name.Should().Be(restaurant.Name);   
            restaruantDto.Id.Should().Be(restaurant.Id);        


        }


    }
}