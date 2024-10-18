using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.Shared.RabbitMq.Events;
using MassTransit;

namespace GymInnowise.EmailService.Logic.Consumers
{
    public class SendTemplateMessageConsumer : IConsumer<SendTemplateMessageEvent>
    {
        private readonly IEmailService _emailService;

        public SendTemplateMessageConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<SendTemplateMessageEvent> context)
        {
            var templateMessage = context.Message.TemplateMessage;
            var result = await _emailService.SendTemplateMessageAsync(templateMessage);
        }
    }
}
