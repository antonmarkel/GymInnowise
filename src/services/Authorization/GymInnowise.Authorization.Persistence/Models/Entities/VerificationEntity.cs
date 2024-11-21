namespace GymInnowise.Authorization.Persistence.Models.Entities
{
    public class VerificationEntity
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}