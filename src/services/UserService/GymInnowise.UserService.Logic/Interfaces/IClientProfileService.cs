using GymInnowise.Shared.User.Dtos.RequestModels.Creates;
using GymInnowise.Shared.User.Dtos.RequestModels.Updates;
using GymInnowise.Shared.User.Dtos.ResponseModels.Gets;
using GymInnowise.UserService.Logic.Results;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Interfaces
{
    public interface IClientProfileService
    {
        Task<OneOf<Success, ProfileAlreadyExists>> CreateClientProfileAsync(Guid accountId,
            CreateClientProfileRequest request);

        Task<OneOf<Success, NotFound>> UpdateClientProfileAsync(Guid accountId,
            UpdateClientProfileRequest request);

        Task<OneOf<Success, NotFound>> UpdateClientProfileStatusAsync(Guid accountId,
            UpdateClientProfileStatusRequest request);

        Task<OneOf<GetClientProfileResponse, NotFound>> GetClientProfileAsync(Guid accountId);
    }
}