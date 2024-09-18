using GymInnowise.GymService.API.Authorization;
using GymInnowise.GymService.Logic.Interfaces;
using GymInnowise.GymService.Shared.Dtos.Requests.Creates;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.GymService.API.Controllers
{
    [ApiController]
    [Route("api/gym-events")]
    public class GymEventsController(IGymEventService _eventService, IAuthorizationService _authorizationService)
        : ControllerBase
    {
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateGymEventAsync([FromBody] CreateGymEventRequest request)
        {
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, request.GymId.ToString(),
                    PolicyNames.GymManagerPolicy);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _eventService.CreateGymEventAsync(request);

            return Created();
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateGymEventAsync(Guid eventId,
            [FromBody] UpdateGymEventRequest request)
        {
            var gymIdResult = await _eventService.GetGymIdAsync(eventId);

            if (gymIdResult.IsT1)
            {
                return NotFound();
            }

            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, gymIdResult.AsT0.ToString(),
                    PolicyNames.GymManagerPolicy);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var result = await _eventService.UpdateGymEventAsync(eventId, request);

            return result.Match<IActionResult>(_ => NoContent(), _ => NotFound());
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{eventId}")]
        public async Task<IActionResult> RemoveGymEventAsync(Guid eventId)
        {
            var gymIdResult = await _eventService.GetGymIdAsync(eventId);

            if (gymIdResult.IsT1)
            {
                return NotFound();
            }

            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, gymIdResult.AsT0.ToString(),
                    PolicyNames.GymManagerPolicy);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
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
