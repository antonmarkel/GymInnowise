using GymInnowise.SectionService.API.Authorization.Requirements;
using GymInnowise.SectionService.Logic.Features.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace GymInnowise.SectionService.API.Authorization.Handlers
{
    public class OwnerHandler : AuthorizationHandler<OwnerRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OwnerRequirement requirement)
        {
            var accountId = ClaimsHelper.GetAccountId(context.User.Claims);
            if (accountId is null)
            {
                return;
            }

            if (accountId.Value == requirement.OwnerId)
            {
                context.Succeed(requirement);
            }
        }
    }
}
