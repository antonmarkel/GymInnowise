using Microsoft.AspNetCore.Authorization;

namespace GymInnowise.UserService.API.Authorization
{
    public class OwnerOrAdminHandler : AuthorizationHandler<OwnerOrAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OwnerOrAdminRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

            var requesterAccountId = context.User.Claims.FirstOrDefault(c => c.Type == "accountId")?.Value;
            var profileId = context.Resource as string;

            if (requesterAccountId != null && profileId != null &&
                requesterAccountId == profileId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
