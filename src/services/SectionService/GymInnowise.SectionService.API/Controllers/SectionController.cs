using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.Shared.Sections.Dtos.Queries;
using GymInnowise.Shared.Sections.Dtos.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.SectionService.API.Controllers
{
    [ApiController]
    [Route("api/sections")]
    public class SectionController : ControllerBase
    {
        private readonly ISender _sender;

        public SectionController(ISender sender)
        {
            _sender = sender;
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
            var result = await _sender.Send(new UpdateSectionCommand(id, request));

            return result.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }
    }
}
