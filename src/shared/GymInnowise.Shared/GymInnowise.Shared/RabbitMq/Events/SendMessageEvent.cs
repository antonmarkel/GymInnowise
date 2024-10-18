using GymInnowise.Shared.Email.Messages;

namespace GymInnowise.Shared.RabbitMq.Events
{
    public class SendMessageEvent
    {
        public required Message EmailMessage { get; set; }
    }
}