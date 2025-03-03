using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.ModifyRestaurant.Tests;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationService;
    private readonly Mock<IRestaurantRepository> _restaurantRepository;

    private readonly UpdateRestaurantCommandHandler _handler;

    public UpdateRestaurantCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _mapper = new Mock<IMapper>();
        _restaurantAuthorizationService = new Mock<IRestaurantAuthorizationService>();
        _restaurantRepository = new Mock<IRestaurantRepository>();

        _handler = new UpdateRestaurantCommandHandler
            (
                _loggerMock.Object,
                _restaurantRepository.Object,
                _mapper.Object,
                _restaurantAuthorizationService.Object
            );

    }

    [Fact()]
    public async Task Handle_ForValidUpdateRestaurantCommand_ChangesSaved()
    {
        // arrange

        var restaurantId = 1;
        var command = new UpdateRestaurantCommand(restaurantId)
        {
            Name = "Test",
            Description = "new Description",
            HasDelivery = true,
        };

        var restaurant = new Restaurant()
        {
            Id = restaurantId,
            Name = "Test",
            Description = "Test"
        };

        _restaurantRepository.Setup(r => r.GetByIdAsync(restaurantId)).ReturnsAsync(restaurant);

        _restaurantAuthorizationService.Setup(m => m.Authorize(restaurant, Domain.Constants.ResourceOperation.Update)).Returns(true);



        // act

        await _handler.Handle(command,CancellationToken.None);

        // assert

        _restaurantRepository.Verify(r=> r.SaveChanges(), Times.Once());
        _mapper.Verify( m=> m.Map(command,restaurant), Times.Once());
    }

    [Fact()]
    public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
    {
        // arrange

        var restaurantId = 2;
        var command = new UpdateRestaurantCommand(restaurantId);
       

        _restaurantRepository.Setup(r => r.GetByIdAsync(restaurantId)).ReturnsAsync((Restaurant?)null);

        // act

        Func<Task> act = async() => await _handler.Handle(command,CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with {restaurantId} does not exist");
    }

    [Fact()]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        // arrange
        var restaurantId = 1;
        var command = new UpdateRestaurantCommand(restaurantId);

        var restaurant = new Restaurant()
        {
            Id = restaurantId,
        };

        _restaurantRepository.Setup(r => r.GetByIdAsync(restaurantId)).ReturnsAsync(restaurant);
        _restaurantAuthorizationService.Setup(m => m.Authorize(restaurant, Domain.Constants.ResourceOperation.Update)).Returns(false);

        // act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // assert
        await act.Should().ThrowAsync<ForbidException>();
    }
}