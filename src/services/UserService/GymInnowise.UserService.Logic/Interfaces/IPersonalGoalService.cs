using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using GymInnowise.UserService.Shared.Dtos.ResponseModels.Gets;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Interfaces
{
    public interface IPersonalGoalService
    {
        Task CreatePersonalGoalAsync(CreatePersonalGoalRequest request);
        Task<OneOf<Success, NotFound>> UpdatePersonalGoalAsync(Guid goalId, UpdatePersonalGoalRequest request);
        Task<OneOf<GetPersonalGoalResponse, NotFound>> GetPersonalGoalAsync(Guid goalId);
        Task<List<GetPersonalGoalResponse>> GetAllPersonalGoalsAsync(Guid ownerId);
        Task<List<GetPersonalGoalResponse>> GetCoachSupervisedGoalsAsync(Guid ownerId, Guid coachId);
    }
}
