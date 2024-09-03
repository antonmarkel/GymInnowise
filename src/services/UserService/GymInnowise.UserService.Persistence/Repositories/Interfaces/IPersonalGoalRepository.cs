using GymInnowise.UserService.Persistence.Models;

namespace GymInnowise.UserService.Persistence.Repositories.Interfaces
{
    public interface IPersonalGoalRepository
    {
        Task CreatePersonalGoalAsync(PersonalGoalModel personalGoalModel);
        Task UpdatePersonalGoalAsync(PersonalGoalModel goalModel);
        Task RemovePersonalGoalAsync(Guid personalGoalId);
        Task<PersonalGoalModel?> GetPersonalGoalAsync(Guid goalId);
        Task<List<PersonalGoalModel>> GetAllPersonalGoalsAsync(Guid accountId);
    }
}
