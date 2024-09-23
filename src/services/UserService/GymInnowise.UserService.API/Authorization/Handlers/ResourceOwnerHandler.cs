using GymInnowise.UserService.API.Authorization.Requirements;
using GymInnowise.UserService.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GymInnowise.UserService.API.Authorization.Handlers
{
    public class ResourceOwnerHandler : AuthorizationHandler<ResourceOwnerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ResourceOwnerRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Admin))
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

            var hasRequiredRole = requirement.Roles.Any(role => context.User.IsInRole(role));
            if (!hasRequiredRole && requirement.Roles.Any())
            {
                return Task.CompletedTask;
            }

            var requesterAccountId =
                context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (requesterAccountId != null && context.Resource is string profileId &&
                requesterAccountId == profileId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
