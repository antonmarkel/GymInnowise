using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class AccountEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
