using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteAllDishes;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetAllDishes;
using Restaurants.Application.Dishes.Queries.GetDishById;

namespace Restaurants.API.Controllers;

[ApiController]
[Authorize]
[Route("api/restaurant/{restaurantId}/dishes")]
public class DishesController(IMediator mediator) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;

        var dishId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetDishById), new { restaurantId , dishId },null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishes([FromRoute] int restaurantId)
    {
        var dishes = await mediator.Send(new GetAllDishesQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<IActionResult> GetDishById([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        var dish = await mediator.Send(new GetDishByIdQuery(restaurantId,dishId));
        return Ok(dish);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllDishes([FromRoute] int restaurantId)
    {
        await mediator.Send(new DeleteAllDishesCommand(restaurantId));
        return NoContent();
    }
}
