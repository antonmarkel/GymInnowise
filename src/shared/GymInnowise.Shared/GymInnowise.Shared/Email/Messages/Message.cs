namespace GymInnowise.Shared.Email.Messages
{
    public class Message
    {
        public required string Receiver { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}