namespace GymInnowise.EmailService.Shared.Configuration
{
    public class RabbitMqSettings
    {
        public required string Host { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
