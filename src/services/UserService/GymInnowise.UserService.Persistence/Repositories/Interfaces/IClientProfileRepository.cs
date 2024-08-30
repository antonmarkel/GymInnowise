using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Shared.Dtos.RequestModels;

namespace GymInnowise.UserService.Persistence.Repositories.Interfaces
{
    public interface IClientProfileRepository
    {
        Task CreateClientProfileAsync(ClientProfileModel clientProfileModel);
        Task<ClientProfileModel?> GetClientProfileByIdAsync(Guid accountId);
        Task UpdateClientProfileAsync(UpdateClientProfileRequest updateClientProfileRequest);
        Task UpdateProfileStatusAsync(UpdateClientProfileStatusRequest updateProfileStatusRequest);
        Task RemoveClientProfileAsync(Guid accountId);
    }
}
