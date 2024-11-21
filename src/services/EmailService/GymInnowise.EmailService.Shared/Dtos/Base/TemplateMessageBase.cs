namespace GymInnowise.EmailService.Shared.Dtos.Base
{
    public abstract class TemplateMessageBase
    {
        public required string Receiver { get; set; }
        public required string TemplateName { get; set; }
        public Dictionary<string, string> Data { get; set; } = [];
    }
}
