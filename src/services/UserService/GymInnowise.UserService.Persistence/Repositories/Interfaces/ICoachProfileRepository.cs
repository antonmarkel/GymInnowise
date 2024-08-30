using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Shared.Dtos.RequestModels;
using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Persistence.Repositories.Interfaces
{
    public interface ICoachProfileRepository
    {
        Task CreateCoachProfileAsync(CoachProfileModel coachProfileModel);
        Task<CoachProfileModel?> GetCoachProfileByIdAsync(Guid accountId);
        Task UpdateCoachProfileAsync(UpdateCoachProfileRequest updateCoachProfileRequest, Guid accountId);
        Task UpdateProfileStatusAsync(UpdateProfileStatusRequest updateProfileStatusRequest, Guid accountId);
        Task UpdateCoachStatusAsync(Guid accountId, CoachStatus status);
        Task RemoveCoachProfileAsync(Guid accountId);
    }
}
