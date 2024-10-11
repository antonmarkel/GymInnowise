using GymInnowise.GymService.Logic.Interfaces;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.Gym.Dtos.Requests.Creates;
using GymInnowise.Shared.Gym.Dtos.Requests.Updates;
using GymInnowise.Shared.Gym.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.GymService.API.Controllers
{
    [ApiController]
    [Route("api/gyms")]
    public class GymsController(IGymService _gymService) : ControllerBase
    {
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateGymAsync([FromBody] CreateGymRequest request)
        {
            await _gymService.CreateGymAsync(request);

            return Created();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{gymId}")]
        public async Task<IActionResult> UpdateGymAsync([FromRoute] Guid gymId,
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
        public async Task<IActionResult> GetGymByIdAsync([FromRoute] Guid gymId)
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
