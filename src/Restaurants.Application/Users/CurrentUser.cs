namespace Restaurants.Application.Users;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles,string? nationality, DateOnly? DateOfBirth)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
