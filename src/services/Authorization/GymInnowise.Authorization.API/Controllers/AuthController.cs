using GymInnowise.Authorization.Logic.Services;
using GymInnowise.Authorization.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.Authorization.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginService _loginService;

        private readonly RegistrationService _registrationService;

        public AuthController(LoginService loginService, RegistrationService registrationService)
        {
            _loginService = loginService;
            _registrationService = registrationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginRequest loginDto)
        {
            var token = await _loginService.Login(loginDto);
            if (string.IsNullOrEmpty(token))
            {

                return Unauthorized("Invalid password!");
            }

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountRegistrationRequest registrationDto)
        {
            await _registrationService.RegisterAccount(registrationDto);
            return Created();
        }
    }
}
