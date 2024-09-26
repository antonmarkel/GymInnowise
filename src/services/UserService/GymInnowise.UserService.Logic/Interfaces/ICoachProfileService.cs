using GymInnowise.Shared.User.Dtos.RequestModels.Creates;
using GymInnowise.Shared.User.Dtos.RequestModels.Updates;
using GymInnowise.Shared.User.Dtos.ResponseModels.Gets;
using GymInnowise.UserService.Logic.Results;
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