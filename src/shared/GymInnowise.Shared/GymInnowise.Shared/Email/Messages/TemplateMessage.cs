using GymInnowise.Shared.Email.Enums;

namespace GymInnowise.Shared.Email.Messages
{
    public class TemplateMessage
    {
        public required string Receiver { get; set; }
        public required string Subject { get; set; }
        public required TemplateEnum Template { get; set; }
        public required object? Model { get; set; }
    }
}
