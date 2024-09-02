using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Shared.Dtos.RequestModels;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Interfaces
{
    public interface IPersonalGoalService
    {
        Task CreateGoalAsync(CreatePersonalGoalRequest request);
        Task<OneOf<Success, GoalNotFound>> UpdatePersonalGoalAsync(UpdatePersonalGoalRequest request);
        Task<OneOf<GetPersonalGoalResponse, GoalNotFound>> GetPersonalGoalAsync(Guid goalId);
        Task<OneOf<Success, GoalNotFound>> RemovePersonalGoalAsync(Guid id);
    }
}
