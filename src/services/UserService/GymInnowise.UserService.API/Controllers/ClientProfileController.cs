using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using GymInnowise.UserService.Shared.Dtos.ResponseModels.Gets;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.UserService.API.Controllers
{
    [ApiController]
    [Route("api/profiles/[controller]")]
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
            await _clientProfileService.CreateClientProfileAsync(request);

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileAsync(Guid id)
        {
            var result = await _clientProfileService.GetClientProfileAsync(id);

            return result.Match<IActionResult>(
                profile => Ok(profile),
                _ => NotFound()
            );
        }


        [HttpPatch("info")]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateClientProfileRequest request)
        {
            var updateResult = await _clientProfileService.UpdateClientProfileAsync(request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }

        [HttpPatch("status")]
        public async Task<IActionResult> UpdateProfileStatus([FromBody] UpdateClientProfileStatusRequest request)
        {
            var updateResult = await _clientProfileService.UpdateClientProfileStatusAsync(request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }
    }
}
