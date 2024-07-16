using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.Authorization.Persistence.Repositories.Implementations
{
    public class RolesRepository : IRolesRepository
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

        public async Task DeleteRoleAsync(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<Role?> GetRoleAsync(string role)
        {
            return await _context.Roles.FirstOrDefaultAsync(v => v.RoleName == role);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            return await _context.Roles.Include(a => a.Accounts).Select(a => new RoleDto
            {
                RoleName = a.RoleName,
                Clients = a.Accounts.Select(a => a.Email).ToArray(),
            }).ToListAsync();
        }
    }
}
