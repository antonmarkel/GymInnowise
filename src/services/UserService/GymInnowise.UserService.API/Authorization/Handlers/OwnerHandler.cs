using GymInnowise.UserService.API.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace GymInnowise.UserService.API.Authorization.Handlers
{
    public class OwnerHandler : AuthorizationHandler<OwnerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OwnerRequirement requirement)
        {
            var requesterAccountId = context.User.Claims.FirstOrDefault(c => c.Type == "accountId")?.Value;
            if (requesterAccountId != null && context.Resource is string coachId &&
                requesterAccountId == coachId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}