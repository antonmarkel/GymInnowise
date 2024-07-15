using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.Authorization.Persistence.Models
{
    public class RoleDto
    {
        public string RoleName { get; set; }
        public string[] Clients { get; set; }

    }
}
