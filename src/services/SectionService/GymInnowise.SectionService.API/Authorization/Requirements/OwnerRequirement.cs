using Microsoft.AspNetCore.Authorization;

namespace GymInnowise.SectionService.API.Authorization.Requirements
{
    public class OwnerRequirement : IAuthorizationRequirement
    {
        public Guid OwnerId { get; set; }
    }
}
