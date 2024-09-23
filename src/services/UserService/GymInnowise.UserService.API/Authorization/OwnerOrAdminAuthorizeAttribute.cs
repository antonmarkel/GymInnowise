using GymInnowise.UserService.Shared.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace GymInnowise.UserService.API.Authorization
{
    public class OwnerOrAdminAuthorizeAttribute(string paramName = "id") : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.IsInRole(Roles.Admin))
            {
                return;
            }

            var accountIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(accountIdClaim) || !Guid.TryParse(accountIdClaim, out Guid userAccountId))
            {
                context.Result = new ForbidResult();

                return;
            }

            if (!context.RouteData.Values.TryGetValue(paramName, out var routeId)
                || !Guid.TryParse(routeId?.ToString(), out Guid resourceId))
            {
                context.Result = new BadRequestResult();

                return;
            }

            if (userAccountId != resourceId)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}