using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UpdateUserEmail;

public class UpdateUserEmailCommandHandler(IUserContext userContext, IUserStore<User> userStore) : IRequestHandler<UpdateUserEmailCommand>
{
    public async Task Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

        if (dbUser == null)
        {
            throw new NotFoundException(nameof(User), user!.Id);
        }

        dbUser.UserName = request.NewUserEmail;
        dbUser.Email = request.NewUserEmail;
        dbUser.NormalizedEmail = request.NewUserEmail.ToUpperInvariant(); // Normalize the email
        dbUser.NormalizedUserName = request.NewUserEmail.ToUpperInvariant(); // Normalize the username

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
