﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Authorization;
/// <summary>
/// Register custom claims to the user
/// </summary>

public class RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<User,IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);
        if(user.Nationality != null)
        {
            id.AddClaim(new Claim(AppClaimTypes.HasNationality, user.Nationality));
        }

        if(user.DateOfBirth != null)
        {
            id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }

        return new ClaimsPrincipal(id);
        
    }
}
