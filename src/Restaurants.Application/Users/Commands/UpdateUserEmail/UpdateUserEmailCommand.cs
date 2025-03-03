using MediatR;

namespace Restaurants.Application.Users.Commands.UpdateUserEmail;

public class UpdateUserEmailCommand : IRequest
{
    public string NewUserEmail { get; set; } = default!;
}
    