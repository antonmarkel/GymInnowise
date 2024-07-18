using GymInnowise.Authorization.Shared.Dtos;

namespace GymInnowise.Authorization.Logic.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string?> LoginAsync(LoginRequest loginDto);
        Task RegisterAsync(RegistrationRequest accountRegistrationDto);
    }
}