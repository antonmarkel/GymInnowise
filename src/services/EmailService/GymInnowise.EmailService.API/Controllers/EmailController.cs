using GymInnowise.EmailService.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.EmailService.API.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailController(IVerificationService _verificationService)
        : ControllerBase
    {
        public const string VerificationEndpoint = "verifyEmail";

        [HttpGet("verify/{token}", Name = VerificationEndpoint)]
        public async Task<IActionResult> VerifyEmailAsync(Guid token)
        {
            var result = await _verificationService.VerifyAsync(token);

            return result.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound(),
                _ => BadRequest("token expired")
            );
        }
    }
}
