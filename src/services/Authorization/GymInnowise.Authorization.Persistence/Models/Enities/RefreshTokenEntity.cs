namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public required string Token { get; set; }
        public required Guid AccountId { get; set; }
        public AccountEntity? Account { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}