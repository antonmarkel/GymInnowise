using GymInnowise.SectionService.API.Authorization.Helpers;
using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.Sections.Dtos.Queries;
using GymInnowise.Shared.Sections.Dtos.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.SectionService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/sections")]
    public class SectionController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly AuthorizationPolicyService _policyHelper;

        public SectionController(ISender sender, AuthorizationPolicyService policyHelper)
        {
            _sender = sender;
            _policyHelper = policyHelper;
        }

        [ActionName(nameof(GetSectionByIdAsync))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSectionByIdAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetSectionPreviewQuery(id));

            return result.Match<IActionResult>(
                Ok,
                _ => NotFound()
            );
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetSectionDetailsByIdAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetSectionFullQuery(id));

            return result.Match<IActionResult>(
                Ok,
                _ => NotFound());
        }

        [HttpGet]
        public async Task<IActionResult> GetSectionsByQuery([FromQuery] SectionsByTagsQuery query)
        {
            var result = await _sender.Send(new GetSectionsByTagsQuery(query.Tags));

            return Ok(result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateSectionAsync([FromBody] CreateSectionRequest request)
        {
            var id = await _sender.Send(new CreateSectionCommand(request));

            return CreatedAtAction(nameof(GetSectionByIdAsync), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSectionByIdAsync([FromRoute] Guid id,
            [FromBody] UpdateSectionRequest request)
        {
            var authResult = await _policyHelper.AuthorizeMentorOrAdminAsync(User, id);
            if (authResult.IsT1)
            {
                return Forbid();
            }

            var result = await _sender.Send(new UpdateSectionCommand(id, request));

            return result.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }
    }
}
