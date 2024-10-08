namespace GymInnowise.EmailService.Shared.Dtos.Events
{
    public record AccountCreatedEvent
    {
        public Guid AccountId { get; set; }
        public required string Email { get; init; }
        public Guid VerificationToken { get; set; }
    }
}
