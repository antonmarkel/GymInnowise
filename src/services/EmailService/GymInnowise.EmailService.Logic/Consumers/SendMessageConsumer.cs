using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Shared.Dtos.Events;
using MassTransit;

namespace GymInnowise.EmailService.API.Features.Consumers
{
    public class SendMessageConsumer(IEmailService _emailService) : IConsumer<SendMessageEvent>
    {
        public async Task Consume(ConsumeContext<SendMessageEvent> context)
        {
            var message = context.Message.EmailMessage;
            await _emailService.SendMessageAsync(message.Receiver, message.Subject, message.Body);
        }
    }
}