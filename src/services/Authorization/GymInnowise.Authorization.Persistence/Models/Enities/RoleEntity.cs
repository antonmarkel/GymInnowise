using GymInnowise.Authorization.Shared.Enums;

namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public required RoleEnum Role { get; set; }
        public List<AccountEntity> Accounts { get; set; } = new();
    }
}
