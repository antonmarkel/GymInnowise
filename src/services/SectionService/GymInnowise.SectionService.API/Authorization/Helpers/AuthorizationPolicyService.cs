using GymInnowise.SectionService.API.Authorization.Requirements;
using GymInnowise.SectionService.Logic.Features.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using OneOf.Types;
using System.Security.Claims;

namespace GymInnowise.SectionService.API.Authorization.Helpers
{
    public class AuthorizationPolicyService
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationPolicyService(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<OneOf<Success, ForbidResult>> AuthorizeMentorOrAdminAsync(ClaimsPrincipal user,
            Guid sectionId)
        {
            var accountId = ClaimsHelper.GetAccountId(user.Claims);
            if (accountId is null)
            {
                return new ForbidResult();
            }

            var requirement = new MentorOrSuperiorRequirement
            {
                AccountId = accountId.Value,
                SectionId = sectionId
            };

            var authResult = await _authorizationService.AuthorizeAsync(user, sectionId, requirement);
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            return new Success();
        }

        public async Task<OneOf<Success, ForbidResult>> AuthorizeOwnerAsync(ClaimsPrincipal user,
            Guid accountId)
        {
            var requirement = new OwnerRequirement()
            {
                OwnerId = accountId
            };

            var authResult = await _authorizationService.AuthorizeAsync(user, accountId, requirement);
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            return new Success();
        }
    }
}