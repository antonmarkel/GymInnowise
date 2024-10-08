using GymInnowise.EmailService.Persistence.Dto;
using GymInnowise.EmailService.Persistence.Models;

namespace GymInnowise.EmailService.Persistence.Repositories.Interfaces
{
    public interface IEmailVerificationRepository
    {
        Task<EmailVerificationEntity?> GetVerificationAsync(Guid token);
        Task RemoveVerificationAsync(EmailVerificationEntity entity);
        Task CreateVerificationAsync(CreateEmailVerification model);
    }
}
