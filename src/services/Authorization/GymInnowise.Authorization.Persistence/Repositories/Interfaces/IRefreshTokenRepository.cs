using GymInnowise.Authorization.Persistence.Models.Enities;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenEntity?> GetRefreshTokenAsync(string token, bool loadAccount = false);
        Task AddRefreshTokenAsync(RefreshTokenEntity refreshToken);
        Task RevokeRefreshTokenAsync(RefreshTokenEntity refreshToken);
    }
}
