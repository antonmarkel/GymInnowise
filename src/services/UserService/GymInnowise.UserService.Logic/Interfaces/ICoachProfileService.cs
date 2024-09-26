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
        Task<OneOf<Success, ProfileAlreadyExists>> CreateCoachProfileAsync(Guid accountId,
            CreateCoachProfileRequest request);

        Task<OneOf<Success, NotFound>> UpdateCoachProfileAsync(Guid accountId, UpdateCoachProfileRequest request);

        Task<OneOf<Success, NotFound>> UpdateCoachProfileStatusAsync(Guid accountId,
            UpdateCoachProfileStatusRequest request);

        Task<OneOf<GetCoachProfileResponse, NotFound>> GetCoachProfileAsync(Guid accountId);
    }
}