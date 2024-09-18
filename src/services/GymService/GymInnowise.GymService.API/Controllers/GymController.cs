using GymInnowise.GymService.Logic.Interfaces;
using GymInnowise.GymService.Shared.Dtos.Requests.Creates;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;
using GymInnowise.GymService.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.GymService.API.Controllers
{
    [ApiController]
    [Route("api/gyms")]
    public class GymsController(IGymService _gymService) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGymAsync([FromBody] CreateGymRequest request)
        {
            await _gymService.CreateGymAsync(request);

            return Created();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{gymId}")]
        public async Task<IActionResult> UpdateGymAsync(Guid gymId,
            [FromBody] UpdateGymRequest request)
        {
            var result = await _gymService.UpdateGymAsync(gymId, request);

            return result.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }

        [Authorize]
        [HttpGet("{gymId}")]
        public async Task<IActionResult> GetGymByIdAsync(Guid gymId)
        {
            var result = await _gymService.GetGymDetailsByIdAsync(gymId);

            return result.Match<IActionResult>(
                resp => Ok(resp),
                _ => NotFound()
            );
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetGymsAsync([FromQuery] List<GymTag>? tags)
        {
            return Ok(await _gymService.GetGymPreviewsByTagsAsync(tags ?? []));
        }
    }
}
