
using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.Email.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.EmailService.API.Controllers
{
    [ApiController]
    [Route("api/email", Name = "Email")]
    public class EmailController(IVerificationService _verificationService, IEmailService _emailService)
        : ControllerBase
    {
        [Authorize(Roles = Roles.Admin)]
        [HttpPost("send")]
        public async Task<IActionResult> SendMessageAsync([FromBody] Message request)
        {
            await _emailService.SendMessageAsync(request.Receiver, request.Subject, request.Body);

            return NoContent();
        }
    }
}
