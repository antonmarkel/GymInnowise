using GymInnowise.SectionService.API.Controllers.Base;
using GymInnowise.Shared.Sections.Base.Relations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.SectionService.API.Controllers
{
    [ApiController]
    [Route("api/membership")]
    public class MembershipController : SectionRelationController<MembershipBase>
    {
        public MembershipController(ISender sender) : base(sender)
        {
        }
    }
}

