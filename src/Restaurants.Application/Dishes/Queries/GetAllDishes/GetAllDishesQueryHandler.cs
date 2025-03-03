

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes;

public class GetAllDishesQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<GetAllDishesQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting all Dishes for id {request.RestaurantId}");
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

        if(restaurant == null )
        {
            throw new NotFoundException($"No restaurant with id: {request.RestaurantId} was found");
        }

        var dishes = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
        return dishes;
    }
}
