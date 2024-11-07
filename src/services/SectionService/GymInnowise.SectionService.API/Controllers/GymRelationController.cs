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
    [Route("api/gym-relation")]
    public class GymRelationController : SectionRelationController<GymRelationBase>
    {
        private readonly AuthorizationPolicyService _policyHelper;

        public GymRelationController(ISender sender, AuthorizationPolicyService policyHelper) : base(sender)
        {
            _policyHelper = policyHelper;
        }
    }
}
