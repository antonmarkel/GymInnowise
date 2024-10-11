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
    public class GymEventsController
        : ControllerBase
    {
        private readonly IGymEventService _eventService;

        public GymEventsController(IGymEventService eventService)
        {
            _eventService = eventService;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateGymEventAsync([FromBody] CreateGymEventDtoRequest dtoRequest)
        {
            var eventId = await _eventService.CreateGymEventAsync(dtoRequest);

            return CreatedAtAction("GetEvent", new { eventId }, eventId);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateGymEventAsync([FromRoute] Guid eventId,
            [FromBody] UpdateGymEventDtoRequest dtoRequest)
        {
            var gymIdResult = await _eventService.GetGymIdAsync(eventId);
            if (gymIdResult.IsT1)
            {
                return NotFound();
            }

            var result = await _eventService.UpdateGymEventAsync(eventId, dtoRequest);

            return result.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{eventId}")]
        public async Task<IActionResult> RemoveGymEventAsync([FromRoute] Guid eventId)
        {
            var gymIdResult = await _eventService.GetGymIdAsync(eventId);
            if (gymIdResult.IsT1)
            {
                return NotFound();
            }

            await _eventService.RemoveGymEventAsync(eventId);

            return NoContent();
        }

        [Authorize]
        [ActionName("GetEvent")]
        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventByIdAsync([FromRoute] Guid eventId)
        {
            var result = await _eventService.GetEventByIdAsync(eventId);

            return result.Match<IActionResult>(
                ev => Ok(ev),
                _ => NotFound()
            );
        }
    }
}