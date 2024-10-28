using Dapper;
using GymInnowise.Shared.User.Enums;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;

namespace GymInnowise.UserService.Persistence.Repositories.Implementations
{
    public class PersonalGoalRepository(DataContext _dataContext) : IPersonalGoalRepository
    {
        public async Task CreatePersonalGoalAsync(PersonalGoalEntity personalGoal)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            INSERT INTO ""PersonalGoals""
            (
                ""Id"", ""Owner"", ""Goal"", ""SupervisorCoach"", 
                ""Status"", ""StartDate"", ""DeadLine""
            )
            VALUES
            (
                @Id, @Owner, @Goal, @SupervisorCoach, 
                @Status, @StartDate, @DeadLine
            );";

            await connection.ExecuteAsync(sql, new
            {
                Id = Guid.NewGuid(),
                personalGoal.Owner,
                personalGoal.Goal,
                personalGoal.SupervisorCoach,
                Status = personalGoal.Status.ToString(),
                personalGoal.StartDate,
                personalGoal.DeadLine,
            });
        }

        public async Task UpdatePersonalGoalAsync(PersonalGoalEntity goal)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            UPDATE ""PersonalGoals""
            SET
                ""Goal"" = @Goal,
                ""SupervisorCoach"" = @SupervisorCoach, ""Status"" = @Status,
                ""StartDate"" = @StartDate, ""DeadLine"" = @DeadLine
            WHERE ""Id"" = @Id;";

            await connection.ExecuteAsync(sql, new
            {
                goal.Id,
                goal.Goal,
                goal.SupervisorCoach,
                Status = goal.Status.ToString(),
                goal.StartDate,
                goal.DeadLine,
            });
        }

        public async Task<List<PersonalGoalEntity>> GetAllPersonalGoalsAsync(Guid accountId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            SELECT * FROM ""PersonalGoals""
            WHERE ""Owner"" = @accountId";

            var result =
                await connection.QueryAsync(sql, new { accountId });

            return result.Select(dn => new PersonalGoalEntity()
            {
                Id = dn.Id,
                Owner = dn.Owner,
                Goal = dn.Goal,
                SupervisorCoach = dn.SupervisorCoach,
                Status = Enum.Parse<GoalStatus>((string)dn.Status),
                StartDate = dn.StartDate,
                DeadLine = dn.DeadLine,
            }).ToList();
        }

        public async Task<PersonalGoalEntity?> GetPersonalGoalAsync(Guid goalId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            SELECT * FROM ""PersonalGoals""
            WHERE ""Id"" = @goalId";

            var result = await connection.QuerySingleOrDefaultAsync(sql, new
            {
                goalId
            });

            return result != null
                ? new PersonalGoalEntity
                {
                    Id = result.Id,
                    Owner = result.Owner,
                    Goal = result.Goal,
                    SupervisorCoach = result.SupervisorCoach,
                    Status = Enum.Parse<GoalStatus>(result.Status),
                    StartDate = result.StartDate,
                    DeadLine = result.DeadLine,
                }
                : result;
        }

        public async Task<List<PersonalGoalEntity>> GetCoachSupervisedGoalsAsync(Guid accountId, Guid coachId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            SELECT * FROM ""PersonalGoals""
            WHERE ""Owner"" = @accountId AND ""SupervisorCoach"" = @coachId";

            var result =
                await connection.QueryAsync(sql, new { accountId, coachId });

            return result.Select(dn => new PersonalGoalEntity()
            {
                Id = dn.Id,
                Owner = dn.Owner,
                Goal = dn.Goal,
                SupervisorCoach = dn.SupervisorCoach,
                Status = Enum.Parse<GoalStatus>((string)dn.Status),
                StartDate = dn.StartDate,
                DeadLine = dn.DeadLine,
            }).ToList();
        }
    }
}