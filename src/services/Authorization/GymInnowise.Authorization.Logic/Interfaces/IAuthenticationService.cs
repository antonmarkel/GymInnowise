using GymInnowise.Authorization.Logic.Results;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;
using GymInnowise.Shared.Authorization.Dtos.ResponseModels;
using OneOf;

namespace GymInnowise.Authorization.Logic.Interfaces
{
    public interface IAuthenticationService
    {
        Task<OneOf<LoginResponse, InvalidCredentials>> LoginAsync(LoginRequest loginRequest);
        Task<OneOf<RefreshResponse, InvalidRefreshToken>> RefreshAsync(RefreshRequest refreshRequest);
        Task RevokeAsync(RevokeRequest revokeRequest);
    }
}