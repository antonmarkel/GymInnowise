namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class AccountEntity
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
