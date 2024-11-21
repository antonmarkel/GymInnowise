using Microsoft.AspNetCore.Authorization;

namespace GymInnowise.SectionService.API.Authorization.Requirements
{
    public class MentorOrSuperiorRequirement : IAuthorizationRequirement
    {
        public Guid SectionId { get; set; }
        public Guid AccountId { get; set; }
    }
}
