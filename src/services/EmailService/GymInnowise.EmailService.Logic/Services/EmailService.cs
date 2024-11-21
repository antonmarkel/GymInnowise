using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.Shared.Email.Messages;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IMessageBuilder _messageBuilder;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IEmailSender emailSender, IMessageBuilder messageBuilder, ILogger<EmailService> logger)
        {
            _emailSender = emailSender;
            _messageBuilder = messageBuilder;
            _logger = logger;
        }

        public async Task SendMessageAsync(Message message)
        {
            await _emailSender.SendEmailAsync(message.Receiver, message.Subject, message.Body);
            _logger.LogInformation("Email was sent to @{message}", message);
        }

        public async Task<OneOf<Success, NotFound>> SendTemplateMessageAsync(TemplateMessage templateMessage)
        {
            var buildMessageResult = await _messageBuilder.BuildMessageFromTemplateAsync(templateMessage);
            if (buildMessageResult.IsT1)
            {
                _logger.LogWarning("Template was not found! @{template}", templateMessage.Template.ToString());

                return new NotFound();
            }

            var message = buildMessageResult.AsT0;
            await SendMessageAsync(message);
            _logger.LogInformation("Email message was successfully sent! @{}", templateMessage);

            return new Success();
        }
    }
}
