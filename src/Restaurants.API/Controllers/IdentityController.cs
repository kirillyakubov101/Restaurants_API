using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRoles;
using Restaurants.Application.Users.Commands.UnAssignUserRole;
using Restaurants.Application.Users.Commands.UpdateUserDetails;
using Restaurants.Application.Users.Commands.UpdateUserEmail;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
       
        [HttpPost("userRole")]
        [Authorize (Roles = UserRoles.Owner)]
        public async Task<IActionResult> AssigneUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent ();
        }
        
        [HttpDelete("deleteUser")]
        [Authorize (Roles = UserRoles.Admin)]
        public async Task<IActionResult> UnAssignUserRole(UnAssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("updateUserEmail")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Owner + "," + UserRoles.User)]
        public async Task<IActionResult> UpdateUserEmail(UpdateUserEmailCommand command)
        {
            await mediator.Send (command);
            return NoContent();
        }
    }
}
