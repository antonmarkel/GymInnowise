using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
