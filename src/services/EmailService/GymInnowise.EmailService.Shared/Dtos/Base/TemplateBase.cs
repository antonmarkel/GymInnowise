namespace GymInnowise.EmailService.Shared.Dtos.Base
{
    public abstract class TemplateBase
    {
        public HashSet<string> Keys { get; set; } = [];
        public required string Body { get; set; }
        public required string Subject { get; set; }
    }
}
