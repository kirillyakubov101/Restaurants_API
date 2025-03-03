using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes;

public class GetAllDishesQuery(int id) : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; } = id;
}
