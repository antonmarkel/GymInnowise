using Microsoft.AspNetCore.Authorization;

namespace GymInnowise.UserService.API.Authorization
{
    public class OwnerOrAdminRequirement : IAuthorizationRequirement
    {
        public OwnerOrAdminRequirement()
        {
        }
    }
}
