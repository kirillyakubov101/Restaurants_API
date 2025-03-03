using MediatR;

namespace Restaurants.Application.Users.Commands.AssignUserRoles;

public class AssignUserRoleCommand : IRequest
{
    public string UserEmails { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}
