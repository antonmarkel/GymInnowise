using Dapper;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Persistence.Repositories.Implementations
{
    public class PersonalGoalRepository(DataContext _dataContext) : IPersonalGoalRepository
    {
        public async Task CreatePersonalGoalAsync(PersonalGoalModel personalGoalModel)
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
                personalGoalModel.Owner,
                personalGoalModel.Goal,
                personalGoalModel.SupervisorCoach,
                Status = personalGoalModel.Status.ToString(),
                personalGoalModel.StartDate,
                personalGoalModel.DeadLine,
            });
        }

        public async Task UpdatePersonalGoalAsync(PersonalGoalModel goalModel)
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
                goalModel.Id,
                goalModel.Goal,
                goalModel.SupervisorCoach,
                Status = goalModel.Status.ToString(),
                goalModel.StartDate,
                goalModel.DeadLine,
            });
        }

        public async Task RemovePersonalGoalAsync(Guid personalGoalId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            DELETE FROM ""PersonalGoals""
            WHERE ""Id"" = @personalGoalId;";

            await connection.ExecuteAsync(sql, new
            {
                personalGoalId,
            });
        }

        public async Task<List<PersonalGoalModel>> GetAllPersonalGoalsAsync(Guid accountId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            SELECT * FROM ""PersonalGoals""
            WHERE ""Owner"" = @accountId";

            var result =
                await connection.QueryAsync(sql, new { accountId });

            return result.Select(dn => new PersonalGoalModel()
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

        public async Task<PersonalGoalModel?> GetPersonalGoalAsync(Guid goalId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            SELECT * FROM ""PersonalGoals""
            WHERE ""Id"" = @goalId";

            var result = await connection.QuerySingleOrDefaultAsync(sql, new
            {
                goalId
            });

            if (result is null)
            {
                return null;
            }

            return new PersonalGoalModel()
            {
                Id = result.Id,
                Owner = result.Owner,
                Goal = result.Goal,
                SupervisorCoach = result.SupervisorCoach,
                Status = result.Status,
                StartDate = result.StartDate,
                DeadLine = result.DeadLine,
            };
        }
    }
}
