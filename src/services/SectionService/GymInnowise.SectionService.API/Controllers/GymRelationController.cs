using GymInnowise.SectionService.API.Controllers.Base;
using GymInnowise.Shared.Sections.Base.Relations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.SectionService.API.Controllers
{
    [ApiController]
    [Route("api/gym-relation")]
    public class GymRelationController : SectionRelationController<GymRelationBase>
    {
        public GymRelationController(ISender sender) : base(sender)
        {
        }
    }
}
