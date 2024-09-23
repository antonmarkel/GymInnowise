using GymInnowise.UserService.API.Authorization;
using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Shared.Authorization;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.UserService.API.Controllers
{
    [ApiController]
    [Route("api/coach-profiles")]
    public class CoachProfileController : ControllerBase
    {
        private readonly ICoachProfileService _coachProfileService;
        private readonly IAuthorizationService _authorizationService;

        public CoachProfileController(ICoachProfileService coachProfileService,
            IAuthorizationService authorizationService)
        {
            _coachProfileService = coachProfileService;
            _authorizationService = authorizationService;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateProfileAsync([FromBody] CreateCoachProfileRequest request)
        {
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, request.AccountId.ToString(),
                    PolicyNames.OwnerPolicy);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var result = await _coachProfileService.CreateCoachProfileAsync(request);

            return result.Match<IActionResult>(
                _ => Created(),
                _ => Conflict("Coach profile connected to this accountId already exists!")
            );
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileAsync(Guid id)
        {
            var result = await _coachProfileService.GetCoachProfileAsync(id);

            return result.Match<IActionResult>(
                profile => Ok(profile),
                _ => NotFound()
            );
        }

        [Authorize(Roles = Roles.Coach)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfileAsync(Guid id, [FromBody] UpdateCoachProfileRequest request)
        {
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, id.ToString(),
                    PolicyNames.OwnerPolicy);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var updateResult = await _coachProfileService.UpdateCoachProfileAsync(id, request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }

        [Authorize(Roles = Roles.CoachOrAdmin)]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateProfileStatusAsync(Guid id,
            [FromBody] UpdateCoachProfileStatusRequest request)
        {
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, id, PolicyNames.OwnerPolicy);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var updateResult = await _coachProfileService.UpdateCoachProfileStatusAsync(id, request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }
    }
}