namespace GymInnowise.EmailService.Persistence.Models
{
    public class EmailVerificationEntity
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
