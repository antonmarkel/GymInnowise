using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Shared.Dtos.RequestModels;

namespace GymInnowise.UserService.Persistence.Repositories.Interfaces
{
    public interface ICoachProfileRepository
    {
        Task CreateCoachProfileAsync(CoachProfileModel coachProfileModel);
        Task<CoachProfileModel?> GetCoachProfileByIdAsync(Guid accountId);
        Task UpdateCoachProfileAsync(CoachProfileModel profileModel);
        Task<bool> DoesAccountExistAsync(Guid accountId);
        Task RemoveCoachProfileAsync(Guid accountId);
    }
}
