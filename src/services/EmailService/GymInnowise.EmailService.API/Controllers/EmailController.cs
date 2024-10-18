using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.Email.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.EmailService.API.Controllers
{
    [ApiController]
    [Route("api/email", Name = "Email")]
    public class EmailController
        : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IMessageBuilder _messageBuilder;

        public EmailController(IEmailService emailService, IMessageBuilder messageBuilder)
        {
            _emailService = emailService;
            _messageBuilder = messageBuilder;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("send")]
        public async Task<IActionResult> SendMessageAsync([FromBody] Message request)
        {
            await _emailService.SendMessageAsync(request);

            return NoContent();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("send/template")]
        public async Task<IActionResult> SendTemplatedMessageAsync([FromBody] TemplateMessage request)
        {
            var result = await _emailService.SendTemplateMessageAsync(request);

            return result.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }
    }
}