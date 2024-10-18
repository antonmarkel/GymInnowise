using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.Shared.Email.Messages;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IMessageBuilder _messageBuilder;

        public EmailService(IEmailSender emailSender, IMessageBuilder messageBuilder)
        {
            _emailSender = emailSender;
            _messageBuilder = messageBuilder;
        }

        public async Task SendMessageAsync(Message message)
        {
            await _emailSender.SendEmailAsync(message.Receiver, message.Subject, message.Body);
        }

        public async Task<OneOf<Success, NotFound>> SendTemplateMessageAsync(TemplateMessage templateMessage)
        {
            var buildMessageResult = await _messageBuilder.BuildMessageFromTemplateAsync(templateMessage);
            if (buildMessageResult.IsT1)
            {
                return new NotFound();
            }

            var message = buildMessageResult.AsT0;
            await SendMessageAsync(message);

            return new Success();
        }
    }
}
