using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.UserService.API.Controllers
{
    [ApiController]
    [Route("api/client-profiles")]
    public class ClientProfileController : ControllerBase
    {
        private readonly IClientProfileService _clientProfileService;

        public ClientProfileController(IClientProfileService clientProfileService)
        {
            _clientProfileService = clientProfileService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfileAsync([FromBody] CreateClientProfileRequest request)
        {
            var result = await _clientProfileService.CreateClientProfileAsync(request);

            return result.Match<IActionResult>(
                _ => Created(),
                _ => Conflict("Profile connected to this accountId already exists!")
            );
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetProfileAsync(Guid clientId)
        {
            var result = await _clientProfileService.GetClientProfileAsync(clientId);

            return result.Match<IActionResult>(
                profile => Ok(profile),
                _ => NotFound()
            );
        }

        [HttpPut("{clientId}")]
        public async Task<IActionResult> UpdateProfileAsync(Guid clientId,
            [FromBody] UpdateClientProfileRequest request)
        {
            var updateResult = await _clientProfileService.UpdateClientProfileAsync(clientId, request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }

        [HttpPut("{clientId}/status")]
        public async Task<IActionResult> UpdateProfileStatusAsync(Guid clientId,
            [FromBody] UpdateClientProfileStatusRequest request)
        {
            var updateResult = await _clientProfileService.UpdateClientProfileStatusAsync(clientId, request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }
    }
}