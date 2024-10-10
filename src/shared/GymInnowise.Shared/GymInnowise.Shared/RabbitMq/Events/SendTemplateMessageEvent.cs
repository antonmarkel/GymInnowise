namespace GymInnowise.Shared.Dtos.Events
{
    public class SendTemplateMessageEvent
    {
        public required string Receiver { get; set; }
        public required string TemplateName { get; set; }
        public Dictionary<string, string> Data { get; set; } = [];
    }
}
