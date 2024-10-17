using GymInnowise.EmailService.Persistence.Data;
using GymInnowise.EmailService.Persistence.Models;
using GymInnowise.EmailService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.EmailService.Persistence.Repositories.Implementations
{
    public class EmailVerificationRepository(EmailServiceContext context) : IEmailVerificationRepository
    {
        private readonly EmailServiceContext _context = context;
        public async Task<EmailVerificationEntity?> GetVerificationAsync(Guid token)
        {
            return await _context.EmailVerifications.FirstOrDefaultAsync(ver => ver.Id == token);
        }

        public async Task CreateVerificationAsync(EmailVerificationEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveVerificationAsync(EmailVerificationEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
