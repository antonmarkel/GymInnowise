using GymInnowise.Authorization.Persistence.Models.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenEntity> GetRefreshTokenAsync(string token);
        Task AddRefreshTokenAsync(RefreshTokenEntity refreshToken);
        Task RevokeRefreshTokenAsync(string token);
    }
}
