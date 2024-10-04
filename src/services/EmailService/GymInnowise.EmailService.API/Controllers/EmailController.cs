using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Shared.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.EmailService.API.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailController(IEmailService _emailService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageRequest request)
        {
            await _emailService.SendMessageAsync(request.Receiver, request.Subject, request.Body);

            return NoContent();
        }

        [HttpPost("template")]
        public async Task<IActionResult> SendTemplateMessageAsync([FromBody] SendTemplateMessageRequest request)
        {
            var result =
                await _emailService.SendTemplateMessageAsync(request.TemplateName, request.Data, request.Receiver);

            return result.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound(),
                _ => BadRequest()
            );
        }
    }
}
