namespace GymInnowise.EmailService.Persistence.Models
{
    public class TemplateEntity
    {
        public required string Name { get; set; }
        public Dictionary<string, string> Data { get; set; } = [];
        public required string Body { get; set; }
        public required string Subject { get; set; }
    }
}
