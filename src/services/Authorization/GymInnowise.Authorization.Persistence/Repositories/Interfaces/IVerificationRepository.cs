using GymInnowise.Authorization.Persistence.Models.Entities;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IVerificationRepository
    {
        Task CreateVerificationAsync(VerificationEntity entity);
        Task<VerificationEntity?> GetVerificationAsync(Guid id);
        Task RemoveVerificationAsync(Guid id);
    }
}
