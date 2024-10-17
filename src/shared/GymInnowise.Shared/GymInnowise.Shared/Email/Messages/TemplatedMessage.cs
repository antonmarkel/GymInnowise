namespace GymInnowise.Shared.Email.Messages
{
    public abstract class TemplatedMessage<T> where T : class
    {
        public required string Receiver { get; set; }
        public required string Subject { get; set; }
        public required string TemplateName { get; set; }
        public required T Model { get; set; }
    }
}
