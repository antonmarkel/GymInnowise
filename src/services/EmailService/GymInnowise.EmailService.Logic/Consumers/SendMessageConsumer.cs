using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.Shared.RabbitMq.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GymInnowise.EmailService.Logic.Consumers
{
    public class SendMessageConsumer : IConsumer<SendMessageEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<SendMessageConsumer> _logger;

        public SendMessageConsumer(IEmailService emailService, ILogger<SendMessageConsumer> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SendMessageEvent> context)
        {
            var message = context.Message.EmailMessage;
            await _emailService.SendMessageAsync(message);
            _logger.LogInformation("Event was consumed:@{eventName}. Message was sent to @{email}",
                nameof(SendMessageEvent), message.Receiver);
        }
    }
}