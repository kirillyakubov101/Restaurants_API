using MediatR;

namespace Restaurants.Application.Users.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommand(string UserEmail,string roleName) : IRequest
{
    public string UserEmail { get; set; } = UserEmail;
    public string RoleName { get; set; } = roleName;
}
    