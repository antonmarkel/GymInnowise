namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; } = null!;
        public List<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();
    }
}
