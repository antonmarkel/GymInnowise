namespace GymInnowise.EmailService.Shared.Dtos.Requests
{
    public class SendMessageRequest
    {
        public required string Receiver { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
