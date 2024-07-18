﻿using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Authorization.Shared.Enums;
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

        public async Task<RoleEntity?> GetRoleAsync(RoleEnum role)
        {
            var roleString = role.ToString();

            return await _context.Roles.FirstOrDefaultAsync(v => roleString.ToLower() == v.RoleName.ToLower());
        }
    }
}
