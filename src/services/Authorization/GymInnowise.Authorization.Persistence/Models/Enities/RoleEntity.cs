namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public required string RoleName { get; set; }
        public List<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();
    }
}
