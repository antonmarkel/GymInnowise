using GymInnowise.Shared.Email.Messages;

namespace GymInnowise.Shared.RabbitMq.Events
{
    public class SendTemplateMessageEvent
    {
        public required TemplateMessage TemplateMessage { get; set; }
    }
}
