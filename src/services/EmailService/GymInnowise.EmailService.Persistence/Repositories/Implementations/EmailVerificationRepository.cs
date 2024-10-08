using GymInnowise.EmailService.Persistence.Data;
using GymInnowise.EmailService.Persistence.Dto;
using GymInnowise.EmailService.Persistence.Models;
using GymInnowise.EmailService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.EmailService.Persistence.Repositories.Implementations
{
    public class EmailVerificationRepository(EmailServiceContext _context) : IEmailVerificationRepository
    {
        public async Task<EmailVerificationEntity?> GetVerificationAsync(Guid token)
        {
            return await _context.EmailVerifications.FirstOrDefaultAsync(ver => ver.Id == token);
        }

        public async Task CreateVerificationAsync(CreateEmailVerification model)
        {
            var entity = new EmailVerificationEntity()
            {
                AccountId = model.AccountId,
                CreatedAt = model.CreatedAt,
                ExpireAt = model.ExpireAt,
                Id = model.Id
            };
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
