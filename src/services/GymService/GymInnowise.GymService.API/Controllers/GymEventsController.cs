using GymInnowise.GymService.Logic.Interfaces;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.Gym.Dtos.Requests.Creates;
using GymInnowise.Shared.Gym.Dtos.Requests.Updates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.GymService.API.Controllers
{
    [ApiController]
    [Route("api/gym-events")]
    public class GymEventsController(IGymEventService _eventService)
        : ControllerBase
    {
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateGymEventAsync([FromBody] CreateGymEventDtoRequest dtoRequest)
        {
            await _eventService.CreateGymEventAsync(dtoRequest);

            return Created();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateGymEventAsync(Guid eventId,
            [FromBody] UpdateGymEventDtoRequest dtoRequest)
        {
            var gymIdResult = await _eventService.GetGymIdAsync(eventId);
            if (gymIdResult.IsT1)
            {
                return NotFound();
            }

            var result = await _eventService.UpdateGymEventAsync(eventId, dtoRequest);

            return result.Match<IActionResult>(_ => NoContent(), _ => NotFound());
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{eventId}")]
        public async Task<IActionResult> RemoveGymEventAsync(Guid eventId)
        {
            var gymIdResult = await _eventService.GetGymIdAsync(eventId);
            if (gymIdResult.IsT1)
            {
                return NotFound();
            }

            await _eventService.RemoveGymEventAsync(eventId);

            return NoContent();
        }

        [HttpGet("{gymId}")]
        public async Task<IActionResult> GetEventsByGymIdAsync(Guid gymId)
        {
            var result = await _eventService.GetEventsByGymIdAsync(gymId);

            return Ok(result);
        }
    }
}