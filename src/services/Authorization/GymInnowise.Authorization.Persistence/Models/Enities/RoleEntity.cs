using System.ComponentModel.DataAnnotations;


namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string RoleName { get; set; } = string.Empty;
        public List<AccountEnity> Accounts { get; set; } = new List<AccountEnity>();
    }
}
