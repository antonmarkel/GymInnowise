namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class AccountEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
