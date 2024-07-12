using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.Authorization.Persistence.Repositories
{
    public class RolesRepository
    {
        private readonly AuthorizationDbContext _context;
        public RolesRepository(AuthorizationDbContext context)
        {
            _context = context;
        }
        public async Task CreateRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }
        public async void DeleteAccountAsync(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}
