using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;
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
                loginResponse => Ok(loginResponse),
                _ => Unauthorized("Invalid password or email!"),
                validError => BadRequest(validError.ErrorMessage)
            );
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshRequest refreshRequest)
        {
            var refreshResult = await _authenticationService.RefreshAsync(refreshRequest);

            return refreshResult.Match<IActionResult>(
                refreshResponse => Ok(refreshResponse),
                _ => Unauthorized("refresh token is invalid")
            );
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
            var registerResult = await _authenticationService.RegisterAsync(registrationDto);

            return registerResult.Match<IActionResult>(
                _ => Created(),
                _ => BadRequest("Account with this email or mobile phone already exists! Try to log in!"),
                validError => BadRequest(validError.ErrorMessage)
            );
        }
    }
}