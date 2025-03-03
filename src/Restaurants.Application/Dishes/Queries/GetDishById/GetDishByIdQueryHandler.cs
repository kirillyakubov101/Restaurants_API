using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishById;

public class GetDishByIdQueryHandler(ILogger<GetDishByIdQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<GetDishByIdQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {

        logger.LogInformation($"Getting Dish id {request.DishId} for restaurant id {request.RestaurantId}");
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant == null)
        {
            throw new NotFoundException($"No restaurant with id: {request.RestaurantId} was found");
        }

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
        if(dish == null)
        {
            throw new NotFoundException($"No dish with id: {request.DishId} was found");
        }
        var result = mapper.Map<DishDto>(dish);
        return result;
    }
}
