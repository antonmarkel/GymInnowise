using GymInnowise.GymService.Logic.Interfaces;
using GymInnowise.GymService.Shared.Dtos.Requests.Creates;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.GymService.API.Controllers
{
    [ApiController]
    [Route("api/gym-events")]
    public class GymEventsController(IGymEventService _eventService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateGymEventAsync([FromBody] CreateGymEventRequest request)
        {
            await _eventService.CreateGymEventAsync(request);

            return Created();
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateGymEventAsync(Guid eventId,
            [FromBody] UpdateGymEventRequest request)
        {
            var result = await _eventService.UpdateGymEventAsync(eventId, request);

            return result.Match<IActionResult>(_ => NoContent(), _ => NotFound());
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> RemoveGymEventAsync(Guid eventId)
        {
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
