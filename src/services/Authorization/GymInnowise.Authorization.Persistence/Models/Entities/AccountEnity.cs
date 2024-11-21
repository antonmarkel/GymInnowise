namespace GymInnowise.Authorization.Persistence.Models.Entities
{
    public class AccountEntity
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<string> Roles { get; set; } = [];
        public bool IsEmailConfirmed { get; set; } = false;
    }
}
