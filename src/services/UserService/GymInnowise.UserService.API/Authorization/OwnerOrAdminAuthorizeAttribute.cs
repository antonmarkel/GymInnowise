using GymInnowise.Shared.Authorization;
using GymInnowise.UserService.Logic.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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

            var userAccountId = ClaimsHelper.GetAccountId(user.Claims);
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