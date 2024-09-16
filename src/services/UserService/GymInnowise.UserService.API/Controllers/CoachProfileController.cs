using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.UserService.API.Controllers
{
    [ApiController]
    [Route("api/coach-profiles")]
    public class CoachProfileController : ControllerBase
    {
        private readonly ICoachProfileService _coachProfileService;

        public CoachProfileController(ICoachProfileService coachProfileService)
        {
            _coachProfileService = coachProfileService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfileAsync([FromBody] CreateCoachProfileRequest request)
        {
            var result = await _coachProfileService.CreateCoachProfileAsync(request);

            return result.Match<IActionResult>(
                _ => Created(),
                _ => Conflict("Coach profile connected to this accountId already exists!")
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileAsync(Guid id)
        {
            var result = await _coachProfileService.GetCoachProfileAsync(id);

            return result.Match<IActionResult>(
                profile => Ok(profile),
                _ => NotFound()
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfileAsync(Guid id, [FromBody] UpdateCoachProfileRequest request)
        {
            var updateResult = await _coachProfileService.UpdateCoachProfileAsync(id, request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateProfileStatusAsync(Guid id,
            [FromBody] UpdateCoachProfileStatusRequest request)
        {
            var updateResult = await _coachProfileService.UpdateCoachProfileStatusAsync(id, request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }
    }
}