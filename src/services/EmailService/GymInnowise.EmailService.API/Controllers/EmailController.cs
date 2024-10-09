using System.ComponentModel;
using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Shared.Dtos.Requests;
using GymInnowise.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.EmailService.API.Controllers
{
    [ApiController]
    [Route("api/email", Name = "Email")]
    public class EmailController(IVerificationService _verificationService, IEmailService _emailService)
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

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("send")]
        public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageRequest request)
        {
            await _emailService.SendMessageAsync(request.Receiver, request.Subject, request.Body);

            return NoContent();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("send/template")]
        public async Task<IActionResult> SendTemplateMessageAsync([FromBody] SendTemplateMessageRequest request)
        {
            var sendingResult =
                await _emailService.SendTemplateMessageAsync(request.Receiver, request.TemplateName, request.Data);

            return sendingResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound(),
                _ => BadRequest()
            );
        }
    }
}
