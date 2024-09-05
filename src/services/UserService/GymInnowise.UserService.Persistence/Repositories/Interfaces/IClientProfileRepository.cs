using GymInnowise.UserService.Persistence.Models;

namespace GymInnowise.UserService.Persistence.Repositories.Interfaces
{
    public interface IClientProfileRepository
    {
        Task CreateClientProfileAsync(ClientProfileModel clientProfileModel);
        Task<ClientProfileModel?> GetClientProfileByIdAsync(Guid accountId);
        Task UpdateClientProfileAsync(ClientProfileModel profileModel);
        Task<bool> DoesProfileExistAsync(Guid accountId);
        Task RemoveClientProfileAsync(Guid accountId);
    }
}
