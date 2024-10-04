namespace GymInnowise.EmailService.Shared.Dtos.Requests
{
    public class SendTemplateMessageRequest
    {
        public required string Receiver { get; set; }
        public required string TemplateName { get; set; }
        public Dictionary<string, string> Data { get; set; } = [];
    }
}
