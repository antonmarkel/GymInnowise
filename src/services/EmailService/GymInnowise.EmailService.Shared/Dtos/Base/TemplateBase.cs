namespace GymInnowise.EmailService.Shared.Dtos.Base
{
    public abstract class TemplateBase
    {
        public Dictionary<string, string> Data { get; set; } = [];
        public required string Body { get; set; }
        public required string Subject { get; set; }
    }
}
