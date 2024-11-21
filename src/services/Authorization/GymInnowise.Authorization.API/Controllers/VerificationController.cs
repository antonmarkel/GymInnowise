using GymInnowise.Authorization.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.Authorization.API.Controllers
{
    [ApiController]
    [Route("api/verification")]
    public class VerificationController : ControllerBase
    {
        private readonly IVerificationService _verificationService;

        public VerificationController(IVerificationService verificationService)
        {
            _verificationService = verificationService;
        }

        [HttpGet("{verificationToken}", Name = IVerificationService.VerificationActionName)]
        public async Task<IActionResult> VerifyAsync(Guid verificationToken)
        {
            var verificationResult = await _verificationService.VerifyAsync(verificationToken);

            return verificationResult.Match<IActionResult>(
                _ => Ok("Email was successfully confirmed"),
                _ => NotFound(),
                _ => BadRequest()
            );
        }
    }
}
