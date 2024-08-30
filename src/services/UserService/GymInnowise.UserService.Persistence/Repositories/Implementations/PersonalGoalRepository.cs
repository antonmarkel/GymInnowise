
using Dapper;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels;

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

        public async Task UpdatePersonalGoalAsync(UpdatePersonalGoalRequest updatePersonalGoalRequest, Guid accountId)
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
                updatePersonalGoalRequest.Id,
                updatePersonalGoalRequest.Goal,
                updatePersonalGoalRequest.SupervisorCoach,
                Status = updatePersonalGoalRequest.Status.ToString(),
                updatePersonalGoalRequest.StartDate,
                updatePersonalGoalRequest.DeadLine,
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

        public async Task<List<PersonalGoalModel>>? GetPersonalGoalsAsync(Guid accountId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            SELECT * FROM ""PersonalGoals""
            WHERE ""Owner"" = @accountId";

            var result =
                await connection.QueryAsync<PersonalGoalModel>(sql, new { accountId });

            return result.ToList();
        }
    }
}
