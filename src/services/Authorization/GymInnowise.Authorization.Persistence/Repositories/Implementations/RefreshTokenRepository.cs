﻿using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models.Enities;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.Authorization.Persistence.Repositories.Implementations
{
    public class RefreshTokenRepository
    {
        private readonly AuthorizationDbContext _context;
        public RefreshTokenRepository(AuthorizationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshTokenEntity?> GetRefreshTokenAsync(string token,bool loadAccount = false)
        {
            var query = _context.RefreshTokens.AsQueryable();
            if (loadAccount)
            {
                query = query.Include(rf => rf.Account);
            }

            return await query.SingleOrDefaultAsync(
                rf => rf.Token == token);
        }

        public async Task AddRefreshTokenAsync(RefreshTokenEntity refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeRefreshTokenAsync(RefreshTokenEntity refreshToken)
        {
            refreshToken.IsRevoked = true;
            await _context.SaveChangesAsync();
        }
    }
}
