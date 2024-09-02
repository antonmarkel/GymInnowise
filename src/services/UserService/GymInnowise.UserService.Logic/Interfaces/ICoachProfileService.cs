using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Shared.Dtos.RequestModels;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Interfaces
{
    public interface ICoachProfileService
    {
        Task CreateClientProfileAsync(CreateCoachProfileRequest request);
        Task<OneOf<Success, AccountNotFound>> UpdateClientProfileAsync(UpdateCoachProfileRequest request);
        Task<OneOf<Success, AccountNotFound>> UpdateProfileStatusAsync(UpdateCoachProfileStatusRequest request);
        Task<OneOf<GetCoachProfileResponse, AccountNotFound>> GetCoachProfileAsync(Guid id);
        Task<OneOf<Success, AccountNotFound>> RemoveClientProfileAsync(Guid id);
    }
}
