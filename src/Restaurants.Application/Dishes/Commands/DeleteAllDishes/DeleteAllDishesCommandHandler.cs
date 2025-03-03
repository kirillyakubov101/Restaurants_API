using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteAllDishes
{
    public class DeleteAllDishesCommandHandler(ILogger<DeleteAllDishesCommandHandler> logger, IRestaurantRepository restaurantRepository,
        IDishesRepository dishesRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteAllDishesCommand>
    {
        public async Task Handle(DeleteAllDishesCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException($"No restaurant with id: {request.RestaurantId} was found");
            }

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            {
                throw new ForbidException();
            }

            await dishesRepository.DeleteAll(restaurant.Dishes);
        }
    }
}
