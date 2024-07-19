using GymInnowise.Authorization.Shared.Dtos.RequestModels;
using GymInnowise.Authorization.Shared.Dtos.ResponseModels;

namespace GymInnowise.Authorization.Logic.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<RegisterResponse> RegisterAsync(RegisterRequest accountRegistrationDto);
        Task<RefreshResponse> RefreshAsync(RefreshRequest refreshRequest);
        Task<RevokeResponse> RevokeAsync(RevokeRequest revokeRequest);
    }
}