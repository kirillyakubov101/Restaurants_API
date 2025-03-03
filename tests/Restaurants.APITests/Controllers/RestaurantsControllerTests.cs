using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.APITests;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System.Net.Http.Json;
using Xunit;

namespace Restaurants.API.Controllers.Tests
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IRestaurantRepository> _restaurantsRepositoryMock = new();
        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator,FakePolicyEvaluator>();
                    services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantRepository), _ => _restaurantsRepositoryMock.Object));
                });
            });
        }


        [Fact()]
        public async Task GetAll_ForValidRequest_Returns200ok()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var result = await client.GetAsync("/api/restaurants?pageNumber=1&pageSize=10");
            
            // assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact()]
        public async Task GetAll_ForInValidRequest_Returns400BadRequest()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var result = await client.GetAsync("/api/restaurants");

            // assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact()]
        public async Task GeById_ForNonExistingId_ShouldReturn404NotFound()
        {
            // arrange

            var id = 1;

            _restaurantsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync((Restaurant?)null);

            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync($"/api/restaurants/{id}");

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact()]
        public async Task GeById_ForExistingId_ShouldReturn200Ok()
        {
            // arrange

            var id = 1;
            var restaurant = new Restaurant()
            {
                Id = id,
                Name = "Test",
                Description = "Test"
            };

            _restaurantsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(restaurant);

            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync($"/api/restaurants/{id}");
            var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            restaurantDto.Should().NotBeNull();
            restaurantDto.Name.Should().Be("Test");
        }
    }
}