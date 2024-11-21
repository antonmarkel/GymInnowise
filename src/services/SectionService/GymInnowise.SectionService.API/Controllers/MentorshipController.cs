using GymInnowise.SectionService.API.Authorization.Helpers;
using GymInnowise.SectionService.API.Controllers.Base;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.Sections.Base.Relations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.SectionService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/mentorship")]
    public class MentorshipController : SectionRelationController<MentorshipBase>
    {
        private readonly AuthorizationPolicyService _policyHelper;

        public MentorshipController(ISender sender, AuthorizationPolicyService policyHelper) : base(sender)
        {
            _policyHelper = policyHelper;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public override Task<IActionResult> CreateRelationAsync(MentorshipBase relation)
        {
            return base.CreateRelationAsync(relation);
        }

        [HttpPut]
        public override async Task<IActionResult> UpdateRelationAsync(MentorshipBase relation)
        {
            var ownerResult = await _policyHelper.AuthorizeOwnerAsync(User, relation.RelatedId);
            if (ownerResult.IsT1 && !User.IsInRole(Roles.Admin))
            {
                return Forbid();
            }

            return await base.UpdateRelationAsync(relation);
        }

        [HttpDelete("{sectionId}/{relatedId}")]
        public override async Task<IActionResult> RemoveRelationAsync(Guid sectionId, Guid relatedId)
        {
            var ownerResult = await _policyHelper.AuthorizeOwnerAsync(User, relatedId);
            if (ownerResult.IsT1 && !User.IsInRole(Roles.Admin))
            {
                return Forbid();
            }

            return await base.RemoveRelationAsync(sectionId, relatedId);
        }
    }
}
