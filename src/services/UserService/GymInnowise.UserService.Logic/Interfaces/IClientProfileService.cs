using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Shared.Dtos.RequestModels;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Interfaces
{
    public interface IClientProfileService
    {
        Task CreateClientProfileAsync(CreateClientProfileRequest request);
        Task<OneOf<Success, AccountNotFound>> UpdateClientProfileAsync(UpdateClientProfileRequest request);
        Task<OneOf<GetClientProfileResponse, AccountNotFound>> GetClientProfileAsync(Guid id);
        Task<OneOf<Success, AccountNotFound>> RemoveClientProfileAsync(Guid id);

        Task<OneOf<Success, AccountNotFound>> UpdateProfileStatusAsync(
            UpdateClientProfileStatusRequest request);
    }
}