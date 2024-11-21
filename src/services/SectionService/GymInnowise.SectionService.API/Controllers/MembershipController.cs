using GymInnowise.SectionService.API.Authorization.Helpers;
using GymInnowise.SectionService.API.Controllers.Base;
using GymInnowise.Shared.Sections.Base.Relations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.SectionService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/membership")]
    public class MembershipController : SectionRelationController<MembershipBase>
    {
        private readonly AuthorizationPolicyService _policyHelper;

        public MembershipController(ISender sender, AuthorizationPolicyService policyHelper) : base(sender)
        {
            _policyHelper = policyHelper;
        }

        [HttpPost]
        public override async Task<IActionResult> CreateRelationAsync([FromBody] MembershipBase relation)
        {
            var authResult = await _policyHelper.AuthorizeMentorOrAdminAsync(User, relation.SectionId);
            if (authResult.IsT1)
            {
                return Forbid();
            }

            return await base.CreateRelationAsync(relation);
        }

        [HttpPut]
        public override async Task<IActionResult> UpdateRelationAsync([FromBody] MembershipBase relation)
        {
            var ownerResult = await _policyHelper.AuthorizeOwnerAsync(User, relation.RelatedId);
            if (ownerResult.IsT1)
            {
                var mentorOrSuperiorResult = await _policyHelper.AuthorizeMentorOrAdminAsync(User, relation.SectionId);
                if (mentorOrSuperiorResult.IsT1)
                {
                    return Forbid();
                }
            }

            return await base.UpdateRelationAsync(relation);
        }

        [HttpDelete("{sectionId}/{relatedId}")]
        public override async Task<IActionResult> RemoveRelationAsync([FromRoute] Guid sectionId,
            [FromRoute] Guid relatedId)
        {
            var ownerResult = await _policyHelper.AuthorizeOwnerAsync(User, relatedId);
            if (ownerResult.IsT1)
            {
                var mentorOrSuperiorResult = await _policyHelper.AuthorizeMentorOrAdminAsync(User, sectionId);
                if (mentorOrSuperiorResult.IsT1)
                {
                    return Forbid();
                }
            }

            return await base.RemoveRelationAsync(sectionId, relatedId);
        }
    }
}