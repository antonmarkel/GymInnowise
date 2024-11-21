namespace GymInnowise.EmailService.Shared.Dtos.Base
{
    public abstract class MessageBase
    {
        public required string Receiver { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
