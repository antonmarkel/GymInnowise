using GymInnowise.UserService.Persistence.Models.Abstract;

namespace GymInnowise.UserService.Persistence.Repositories.Interfaces
{
    public interface IProfileRepository<T> where T : ProfileEntity
    {
        Task CreateProfileAsync(T profile);
        Task<T?> GetProfileByIdAsync(Guid accountId);
        Task UpdateProfileAsync(T profile);
        Task<bool> DoesProfileExistAsync(Guid accountId);
    }
}
