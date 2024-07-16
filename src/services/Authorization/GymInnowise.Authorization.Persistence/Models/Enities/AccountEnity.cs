using System.ComponentModel.DataAnnotations;

namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class AccountEnity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<RoleEntity> Roles { get; set; } = new List<RoleEntity>();

    }
}
