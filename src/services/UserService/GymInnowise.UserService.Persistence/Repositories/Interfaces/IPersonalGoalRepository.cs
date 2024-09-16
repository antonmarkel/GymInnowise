using GymInnowise.UserService.Persistence.Models;

namespace GymInnowise.UserService.Persistence.Repositories.Interfaces
{
    public interface IPersonalGoalRepository
    {
        Task CreatePersonalGoalAsync(PersonalGoalEntity personalGoal);
        Task UpdatePersonalGoalAsync(PersonalGoalEntity goal);
        Task<PersonalGoalEntity?> GetPersonalGoalAsync(Guid goalId);
        Task<List<PersonalGoalEntity>> GetAllPersonalGoalsAsync(Guid accountId);
        Task<List<PersonalGoalEntity>> GetCoachSupervisedGoalsAsync(Guid accountId, Guid coachId);
    }
}
