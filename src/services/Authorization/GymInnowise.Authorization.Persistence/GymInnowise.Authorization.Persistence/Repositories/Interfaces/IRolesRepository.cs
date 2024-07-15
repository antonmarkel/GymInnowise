using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        Task CreateRoleAsync(Role role);
        void DeleteAccountAsync(Role role);
        Task<Role> GetRoleAsync(string role);
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    }
}
