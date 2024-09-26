using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.Authorization.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            var loginResult = await _authenticationService.LoginAsync(loginRequest);

            return loginResult.Match<IActionResult>(
                Ok,
                _ => Unauthorized("Invalid password or email!")
            );
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshRequest refreshRequest)
        {
            var refreshResult = await _authenticationService.RefreshAsync(refreshRequest);

            return refreshResult.Match<IActionResult>(
                Ok,
                _ => Unauthorized("Refresh token is invalid!")
            );
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeAsync([FromBody] RevokeRequest revokeRequest)
        {
            await _authenticationService.RevokeAsync(revokeRequest);

            return Ok();
        }
    }
}