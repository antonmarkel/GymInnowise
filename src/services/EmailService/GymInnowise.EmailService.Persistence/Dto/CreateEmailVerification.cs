namespace GymInnowise.EmailService.Persistence.Dto
{
    public class CreateEmailVerification
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Guid AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
