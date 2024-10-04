namespace GymInnowise.EmailService.Persistence.Models
{
    public class TemplateEntity
    {
        public required string Name { get; set; }
        public List<string> Data { get; set; } = [];
        public required string Body { get; set; }
        public required string Subject { get; set; }
    }
}
