using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models.Entities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.Authorization.Persistence.Repositories.Implementations
{
    public class VerificationRepository : IVerificationRepository
    {
        private readonly AuthorizationDbContext _context;

        public VerificationRepository(AuthorizationDbContext context)
        {
            _context = context;
        }

        public async Task CreateVerificationAsync(VerificationEntity entity)
        {
            await _context.Verifications.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveVerificationAsync(Guid id)
        {
            await _context.Verifications.Where(ver => ver.Id == id).ExecuteDeleteAsync();
        }

        public async Task<VerificationEntity?> GetVerificationAsync(Guid id)
        {
            var entity = await _context.Verifications.FirstOrDefaultAsync(ver => ver.Id == id);

            return entity;
        }
    }
}
