using GymInnowise.Shared.Email.Messages;

namespace GymInnowise.EmailService.Shared.Dtos.Events
{
    public class SendTemplateMessageEvent<T> where T : class
    {
        public required TemplatedMessage<T> TemplatedMessage { get; set; }
    }
}
