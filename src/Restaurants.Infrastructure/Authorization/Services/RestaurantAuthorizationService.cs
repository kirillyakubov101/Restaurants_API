using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOptions)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Auhtorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
            user.Email,
            resourceOptions,
            restaurant.Name);

        if (resourceOptions == ResourceOperation.Read || resourceOptions == ResourceOperation.Create)
        {
            logger.LogInformation("Create/read operaion - successful authorization");
            return true;
        }

        if (resourceOptions == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }

        if ((resourceOptions == ResourceOperation.Delete || resourceOptions == ResourceOperation.Update) &&
            user.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Restaurant owner - successful authorization");
            return true;
        }

        return false;
    }

}
