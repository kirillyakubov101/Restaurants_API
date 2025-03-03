using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.AssignUserRoles;

public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigning user role: {@Request}", request);
        var user = await userManager.FindByEmailAsync(request.UserEmails);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.UserEmails);
        }

        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            throw new NotFoundException(nameof(IdentityRole), request.RoleName);
        }

        //assign the role to the user
        await userManager.AddToRoleAsync(user, role.Name!);
    }
}
