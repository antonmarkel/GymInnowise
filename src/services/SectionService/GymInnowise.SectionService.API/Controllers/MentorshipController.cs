using GymInnowise.SectionService.API.Controllers.Base;
using GymInnowise.Shared.Sections.SectionRelations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.SectionService.API.Controllers
{
    [ApiController]
    [Route("api/mentorship")]
    public class MentorshipController : SectionRelationController<Mentorship>
    {
        public MentorshipController(ISender sender) : base(sender)
        {
        }
    }
}
