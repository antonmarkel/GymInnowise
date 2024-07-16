using System.ComponentModel.DataAnnotations;


namespace GymInnowise.Authorization.Persistence.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public List<Account> Accounts { get; set; } 
    }
}
