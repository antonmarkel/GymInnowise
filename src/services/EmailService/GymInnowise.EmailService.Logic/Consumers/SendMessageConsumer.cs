using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.Shared.RabbitMq.Events;
using MassTransit;

namespace GymInnowise.EmailService.API.Features.Consumers
{
    public class SendMessageConsumer : IConsumer<SendMessageEvent>
    {
        private readonly IEmailService _emailService;

        public SendMessageConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<SendMessageEvent> context)
        {
            var message = context.Message.EmailMessage;
            await _emailService.SendMessageAsync(message);
        }
    }
}