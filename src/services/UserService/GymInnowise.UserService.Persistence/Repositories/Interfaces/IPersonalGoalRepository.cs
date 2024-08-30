using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Shared.Dtos.RequestModels;

namespace GymInnowise.UserService.Persistence.Repositories.Interfaces
{
    public interface IPersonalGoalRepository
    {
        Task CreatePersonalGoalAsync(PersonalGoalModel personalGoalModel);
        Task UpdatePersonalGoalAsync(UpdatePersonalGoalRequest updatePersonalGoalRequest, Guid accountId);
        Task RemovePersonalGoalAsync(Guid personalGoalId);
        Task<List<PersonalGoalModel>>? GetPersonalGoalsAsync(Guid accountId);
    }
}
