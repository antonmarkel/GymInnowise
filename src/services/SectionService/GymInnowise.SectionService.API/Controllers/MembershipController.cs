using GymInnowise.SectionService.API.Controllers.Base;
using GymInnowise.Shared.Sections.SectionRelations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.SectionService.API.Controllers
{
    [Route("api/membership")]
    public class MembershipController : SectionRelationController<Membership>
    {
        public MembershipController(ISender sender) : base(sender)
        {
        }
    }
}

