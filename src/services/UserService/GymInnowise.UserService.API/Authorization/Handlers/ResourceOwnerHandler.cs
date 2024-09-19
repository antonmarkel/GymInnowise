using GymInnowise.UserService.API.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace GymInnowise.UserService.API.Authorization.Handlers
{
    public class ResourceOwnerHandler : AuthorizationHandler<ResourceOwnerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ResourceOwnerRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

            bool hasRequiredRole = requirement.Roles.Any(role => context.User.IsInRole(role));
            if (!hasRequiredRole)
            {
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
