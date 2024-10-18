namespace GymInnowise.Shared.RabbitMq.Events
{
    public record AccountCreatedEvent
    {
        public Guid AccountId { get; set; }
        public required string Email { get; init; }
    }
}
