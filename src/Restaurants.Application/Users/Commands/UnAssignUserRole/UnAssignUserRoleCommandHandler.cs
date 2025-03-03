using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommandHandler(ILogger<UnAssignUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<UnAssignUserRoleCommand>
{
    public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.UserEmail);
        if(user == null)
        {
            throw new NotFoundException(nameof(User), request.UserEmail);
        }

        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            throw new NotFoundException(nameof(IdentityRole), request.RoleName);
        }

        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}
