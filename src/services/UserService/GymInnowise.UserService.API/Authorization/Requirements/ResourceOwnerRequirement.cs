using Microsoft.AspNetCore.Authorization;

namespace GymInnowise.UserService.API.Authorization.Requirements
{
    public class ResourceOwnerRequirement(string[]? roles = null) : IAuthorizationRequirement
    {
        public string[] Roles { get; set; } = roles ?? [];
    }
}
