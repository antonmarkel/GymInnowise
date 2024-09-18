using GymInnowise.GymService.API.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GymInnowise.GymService.API.Authorization.Handlers
{
    public class GymManagerHandler : AuthorizationHandler<GymManagerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            GymManagerRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

            if (context.User.IsInRole("Manager"))
            {
                var requesterGymId = context.User.Claims.FirstOrDefault(c => c.Type == "gymId")?.Value;
                if (requesterGymId != null && context.Resource is string gymId &&
                    requesterGymId == gymId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}