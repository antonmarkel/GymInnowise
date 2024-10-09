using GymInnowise.EmailService.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.EmailService.API.Controllers
{
    [ApiController]
    [Route("api/email", Name = "Email")]
    public class EmailController(IVerificationService _verificationService)
        : ControllerBase
    {
        public const string VerificationEndpoint = "VerifyEmail";

        [HttpGet("verify/{token}", Name = VerificationEndpoint)]
        public async Task<IActionResult> VerifyEmailAsync(Guid token)
        {
            var result = await _verificationService.VerifyAsync(token);

            return result.Match<IActionResult>(
                _ => Ok("Email successfully confirmed"),
                _ => NotFound(),
                _ => BadRequest("token expired")
            );
        }
    }
}
