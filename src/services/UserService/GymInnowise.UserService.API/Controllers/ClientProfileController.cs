using GymInnowise.UserService.API.Authorization;
using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.UserService.API.Controllers
{
    [ApiController]
    [Route("api/client-profiles")]
    public class ClientProfileController : ControllerBase
    {
        private readonly IClientProfileService _clientProfileService;
        private readonly IAuthorizationService _authorizationService;

        public ClientProfileController(IClientProfileService clientProfileService,
            IAuthorizationService authorizationService)
        {
            _clientProfileService = clientProfileService;
            _authorizationService = authorizationService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProfileAsync([FromBody] CreateClientProfileRequest request)
        {
            var result = await _clientProfileService.CreateClientProfileAsync(request);

            return result.Match<IActionResult>(
                _ => Created(),
                _ => Conflict("Profile connected to this accountId already exists!")
            );
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileAsync(Guid id)
        {
            var result = await _clientProfileService.GetClientProfileAsync(id);

            return result.Match<IActionResult>(
                profile => Ok(profile),
                _ => NotFound()
            );
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfileAsync(Guid id,
            [FromBody] UpdateClientProfileRequest request)
        {
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, id.ToString(),
                    PolicyNames.OwnerOrAdmin);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var updateResult = await _clientProfileService.UpdateClientProfileAsync(id, request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }

        [Authorize]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateProfileStatusAsync(Guid id,
            [FromBody] UpdateClientProfileStatusRequest request)
        {
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, id.ToString(), PolicyNames.OwnerOrAdmin);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var updateResult = await _clientProfileService.UpdateClientProfileStatusAsync(id, request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }
    }
}