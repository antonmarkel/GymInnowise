using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.Authorization.Persistence.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get;set; }
        [Required]
        public string PhoneNumber { get; set;}
        public string PasswordHash { get; set;}
        public DateTime CreatedDate { get;set; }
        public DateTime ModifiedDate { get;set; }

        public List<Role> Roles { get; set; }

    }
}
