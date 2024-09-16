using GymInnowise.UserService.API.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace GymInnowise.UserService.API.Authorization.Handlers
{
    public class SupervisorHandler : AuthorizationHandler<SupervisorRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SupervisorRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

            var requesterAccountId = context.User.Claims.FirstOrDefault(c => c.Type == "accountId")?.Value;

            if (requesterAccountId != null && context.Resource is string profileId &&
                requesterAccountId == profileId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
