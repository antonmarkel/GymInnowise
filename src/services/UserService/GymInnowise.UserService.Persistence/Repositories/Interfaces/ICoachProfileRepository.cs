using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Shared.Dtos.RequestModels;

namespace GymInnowise.UserService.Persistence.Repositories.Interfaces
{
    public interface ICoachProfileRepository
    {
        Task CreateCoachProfileAsync(CoachProfileModel coachProfileModel);
        Task<CoachProfileModel?> GetCoachProfileByIdAsync(Guid accountId);
        Task UpdateCoachProfileAsync(UpdateCoachProfileRequest updateCoachProfileRequest);
        Task UpdateProfileStatusAsync(UpdateCoachProfileStatusRequest updateProfileStatusRequest);
        Task RemoveCoachProfileAsync(Guid accountId);
    }
}
