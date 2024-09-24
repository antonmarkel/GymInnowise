using GymInnowise.Authorization.Logic.Services.Results;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;
using GymInnowise.Authorization.Shared.Dtos.ResponseModels;
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