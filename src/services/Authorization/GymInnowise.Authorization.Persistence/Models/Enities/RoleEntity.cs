using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public List<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();
    }
}
