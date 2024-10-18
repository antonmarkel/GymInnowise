using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.Shared.Email.Messages;
using GymInnowise.Shared.RabbitMq.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GymInnowise.EmailService.Logic.Consumers
{
    public class SendTemplateMessageConsumer : IConsumer<SendTemplateMessageEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<SendTemplateMessageConsumer> _logger;

        public SendTemplateMessageConsumer(IEmailService emailService, ILogger<SendTemplateMessageConsumer> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SendTemplateMessageEvent> context)
        {
            var templateMessage = context.Message.TemplateMessage;
            var result = await _emailService.SendTemplateMessageAsync(templateMessage);
            result.Switch(
                _ => _logger.LogInformation("Event was consumed:@{eventName}. Message was sent to @{email}",
                    nameof(SendMessageEvent), templateMessage.Receiver),
                _ => _logger.LogInformation("Event was consumed:@{eventName}. Message was NOT sent to @{email}",
                    nameof(SendMessageEvent), templateMessage.Receiver));
        }
    }
}
