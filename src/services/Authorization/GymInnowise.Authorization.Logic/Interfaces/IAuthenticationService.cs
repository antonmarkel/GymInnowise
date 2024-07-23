using GymInnowise.Authorization.Logic.Services.Results;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;
using GymInnowise.Authorization.Shared.Dtos.ResponseModels;
using OneOf;
using OneOf.Types;

namespace GymInnowise.Authorization.Logic.Interfaces
{
    public interface IAuthenticationService
    {
        Task<OneOf<LoginResponse, InvalidCredentials, AccountValidationError>> LoginAsync(LoginRequest loginRequest);

        Task<OneOf<Success, AccountAlreadyExists, AccountValidationError>> RegisterAsync(
            RegisterRequest registerRequest);

        Task<OneOf<RefreshResponse, InvalidRefreshToken>> RefreshAsync(RefreshRequest refreshRequest);
        Task RevokeAsync(RevokeRequest revokeRequest);
    }
}