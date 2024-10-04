namespace GymInnowise.EmailService.Persistence.Models
{
    public class MessageTemplateEntity
    {
        public required string Name { get; set; }
        public Dictionary<string, string> Data { get; set; } = [];
    }
}
