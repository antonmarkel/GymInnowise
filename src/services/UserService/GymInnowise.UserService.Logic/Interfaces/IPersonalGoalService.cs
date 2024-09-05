using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Interfaces
{
    public interface IPersonalGoalService
    {
        Task CreatePersonalGoalAsync(CreatePersonalGoalRequest request);
        Task<OneOf<Success, GoalNotFound>> UpdatePersonalGoalAsync(UpdatePersonalGoalRequest request);
        Task<List<PersonalGoalModel>> GetAllPersonalGoalsAsync(Guid ownerId);
        Task RemovePersonalGoalAsync(Guid id);
    }
}
