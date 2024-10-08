using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Shared.Dtos.Events;
using MassTransit;

namespace GymInnowise.EmailService.API.Features.Consumers
{
    public class SendTemplateMessageConsumer(IEmailService _emailService) : IConsumer<SendTemplateMessageEvent>
    {
        public async Task Consume(ConsumeContext<SendTemplateMessageEvent> context)
        {
            var message = context.Message;
            await _emailService.SendTemplateMessageAsync(message.Receiver, message.TemplateName, message.Data);
        }
    }
}
