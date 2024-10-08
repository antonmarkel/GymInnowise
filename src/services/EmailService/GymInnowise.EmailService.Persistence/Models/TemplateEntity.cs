namespace GymInnowise.EmailService.Persistence.Models
{
    public class TemplateEntity
    {
        public required string Name { get; set; }
        public HashSet<string> Data { get; set; } = [];
        public required string Body { get; set; }
        public required string Subject { get; set; }
    }
}
