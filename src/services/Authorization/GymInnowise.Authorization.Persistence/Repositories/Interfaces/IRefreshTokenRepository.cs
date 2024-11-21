using GymInnowise.Authorization.Persistence.Models.Entities;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenEntity?> GetRefreshTokenAsync(string token, bool loadAccount = false);
        Task AddRefreshTokenAsync(RefreshTokenEntity refreshToken);
        Task DeleteRefreshTokenAsync(RefreshTokenEntity refreshToken);
    }
}
