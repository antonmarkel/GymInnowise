namespace GymInnowise.Shared.Dtos.Events
{
    public class SendMessageEvent
    {
        public required string Receiver { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}