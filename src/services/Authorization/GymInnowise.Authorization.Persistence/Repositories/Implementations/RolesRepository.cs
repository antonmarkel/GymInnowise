﻿using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Models.Previews;
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

        public async Task CreateRoleAsync(RoleEntity role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(RoleEntity role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<RoleEntity?> GetRoleAsync(string role)
        {
            return await _context.Roles.FirstOrDefaultAsync(v => v.RoleName == role);
        }

        public async Task<IEnumerable<RolePreview>> GetAllRolesAsync()
        {
            return await _context.Roles.Include(a => a.Accounts).Select(a => new RolePreview
            {
                RoleName = a.RoleName,
                Clients = a.Accounts.Select(a => a.Email).ToArray(),
            }).ToListAsync();
        }
    }
}
