﻿using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        // arrange
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var mapperMock = new Mock<IMapper>();
        var userContextMock = new Mock<IUserContext>();
        var restaurantRepoMock = new Mock<IRestaurantRepository>();

        restaurantRepoMock.Setup(repo => repo.Create(It.IsAny<Restaurant>())).ReturnsAsync(1);

        var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(user => user.GetCurrentUser()).Returns(currentUser);

        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();
        mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);

        var commandHandler = new CreateRestaurantCommandHandler
            (
            loggerMock.Object,
            mapperMock.Object, 
            restaurantRepoMock.Object,
            userContextMock.Object
            );

        // act

        var result = await commandHandler.Handle(command, CancellationToken.None );

        // assert

        result.Should().Be(1);
        restaurant.OwnerId.Should().Be(currentUser.Id);
        restaurantRepoMock.Verify(r => r.Create(restaurant),Times.Once());
    }
}