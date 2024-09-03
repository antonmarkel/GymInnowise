using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.UserService.API.Controllers
{
    [ApiController]
    [Route("api/profiles/[controller]")]
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
            await _coachProfileService.CreateCoachProfileAsync(request);

            return Created();
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

        [HttpPatch("info")]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateCoachProfileRequest request)
        {
            var updateResult = await _coachProfileService.UpdateCoachProfileAsync(request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }

        [HttpPatch("status")]
        public async Task<IActionResult> UpdateProfileStatus([FromBody] UpdateCoachProfileStatusRequest request)
        {
            var updateResult = await _coachProfileService.UpdateCoachProfileStatusAsync(request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }
    }
}
