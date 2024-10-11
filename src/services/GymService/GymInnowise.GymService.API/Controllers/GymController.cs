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
    public class GymsController : ControllerBase
    {
        private readonly IGymService _gymService;
        private readonly IGymEventService _eventService;

        public GymsController(IGymService gymService, IGymEventService eventService)
        {
            _gymService = gymService;
            _eventService = eventService;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateGymAsync([FromBody] CreateGymRequest request)
        {
            var gymId = await _gymService.CreateGymAsync(request);

            return CreatedAtAction("GetGym", new { gymId }, gymId);
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
        [HttpGet("{gymId}/events")]
        public async Task<IActionResult> GetEventsByGymIdAsync([FromRoute] Guid gymId)
        {
            var result = await _eventService.GetEventsByGymIdAsync(gymId);

            return Ok(result);
        }

        [ActionName("GetGym")]
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
