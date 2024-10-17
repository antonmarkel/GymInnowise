using GymInnowise.Shared.Email.Messages;

namespace GymInnowise.EmailService.Shared.Dtos.Events
{
    public class SendMessageEvent
    {
        public required Message EmailMessage { get; set; }
    }
}