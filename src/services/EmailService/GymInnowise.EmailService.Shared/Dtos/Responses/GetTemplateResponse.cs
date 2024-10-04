namespace GymInnowise.EmailService.Shared.Dtos.Responses
{
    public class GetTemplateResponse
    {
        public required string TemplateName { get; set; }
        public Dictionary<string, string> Data { get; set; } = [];
        public required string Body { get; set; }
        public required string Subject { get; set; }
    }
}
