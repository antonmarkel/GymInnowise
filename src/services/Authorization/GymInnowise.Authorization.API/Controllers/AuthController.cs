using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            var loginResponse = await _authenticationService.LoginAsync(loginRequest);
            if (loginResponse.AccessToken.IsNullOrEmpty() || loginResponse.RefreshToken.IsNullOrEmpty())
            {
                return Unauthorized("Invalid password or email!");
            }

            return Ok(loginResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshRequest refreshRequest)
        {
            var refreshResponse = await _authenticationService.RefreshAsync(refreshRequest);
            if (refreshResponse.AccessToken.IsNullOrEmpty() || refreshResponse.RefreshToken.IsNullOrEmpty())
            {
                return Unauthorized("refresh token invalid");
            }

            return Ok(refreshResponse);
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeAsync([FromBody] RevokeRequest revokeRequest)
        {
            await _authenticationService.RevokeAsync(revokeRequest);

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registrationDto)
        {
            await _authenticationService.RegisterAsync(registrationDto);

            return Created();
        }
    }
}