using FluentAssertions;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Xunit;


namespace Restaurants.ApplicationTests.Users;

public class CurrentUserTests
{
    // TestMethod_Scenario_ExpectedResult
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRol_WithMathchingRole_ShouldReturnTrue(string roleName)
    {
        // arrange
        var currentUser = new CurrentUser("1", "EMAIL@test.com", [UserRoles.Admin, UserRoles.User],null,null);


        // act

        var isInRole = currentUser.IsInRole(UserRoles.Admin);

        // asset

        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRol_WithNoMathchingRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "EMAIL@test.com", [UserRoles.Admin, UserRoles.User], null, null);


        // act

        var isInRole = currentUser.IsInRole(UserRoles.Owner);

        // asset

        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRol_WithNoMathchingRoleCase_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "EMAIL@test.com", [UserRoles.Admin, UserRoles.User], null, null);


        // act

        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        // asset

        isInRole.Should().BeFalse();
    }
}