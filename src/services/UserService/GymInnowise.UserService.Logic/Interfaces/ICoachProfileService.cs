using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using GymInnowise.UserService.Shared.Dtos.ResponseModels.Gets;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Interfaces
{
    public interface ICoachProfileService
    {
        Task<OneOf<Success, ProfileAlreadyExists>> CreateCoachProfileAsync(CreateCoachProfileRequest request);
        Task<OneOf<Success, ProfileNotFound>> UpdateCoachProfileAsync(UpdateCoachProfileRequest request);
        Task<OneOf<Success, ProfileNotFound>> UpdateCoachProfileStatusAsync(UpdateCoachProfileStatusRequest request);
        Task<OneOf<GetCoachProfileResponse, ProfileNotFound>> GetCoachProfileAsync(Guid accountId);
        Task RemoveCoachProfileAsync(Guid accountId);
    }
}
