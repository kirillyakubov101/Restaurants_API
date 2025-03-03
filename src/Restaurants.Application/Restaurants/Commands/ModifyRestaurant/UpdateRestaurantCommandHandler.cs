using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.ModifyRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
    IRestaurantRepository restaurantRepository,
    IMapper mapper,
    IRestaurantAuthorizationService restaurantAuthorizationService)
    : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Modifying a restaurant with id : {request.Id}");
        var restaurant = await restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
        {
            throw new NotFoundException($"Restaurant with {request.Id} does not exist");
        }

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new ForbidException();
        }

        mapper.Map(request, restaurant);
        //restuarant.Name = request.Name;
        //restuarant.Description = request.Description;
        //restuarant.HasDelivery = request.HasDelivery;
        await restaurantRepository.SaveChanges();
    }
}
 