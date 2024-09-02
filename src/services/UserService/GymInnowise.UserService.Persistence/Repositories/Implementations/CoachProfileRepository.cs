using Dapper;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Enums;
using System.Text.Json;

namespace GymInnowise.UserService.Persistence.Repositories.Implementations
{
    public class CoachProfileRepository(DataContext _dataContext) : ICoachProfileRepository
    {
        public async Task CreateCoachProfileAsync(CoachProfileModel coachProfileModel)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            INSERT INTO ""CoachProfiles""
            (
                ""AccountId"", ""FirstName"", ""LastName"", ""DateOfBirth"", ""Gender"", 
                ""CreatedAt"", ""UpdatedAt"", ""HiredAt"", ""CostPerHour"", ""AccountStatus"", ""StatusNotes"", 
                ""ExpectedReturnDate"", ""CoachStatus"", ""Tags""
            )
            VALUES
            (
                @AccountId, @FirstName, @LastName, @DateOfBirth, @Gender, 
                @CreatedAt, @UpdatedAt, @HiredAt, @CostPerHour, @AccountStatus, @StatusNotes, 
                @ExpectedReturnDate, @Tags, @CoachStatus
            );";

            await connection.ExecuteAsync(sql, new
            {
                AccountId = Guid.NewGuid(),
                coachProfileModel.FirstName,
                coachProfileModel.LastName,
                coachProfileModel.DateOfBirth,
                coachProfileModel.Gender,
                coachProfileModel.CreatedAt,
                coachProfileModel.UpdatedAt,
                AccountStatus = coachProfileModel.AccountStatus.ToString(),
                CoachStatus = coachProfileModel.CoachStatus.ToString(),
                coachProfileModel.CostPerHour,
                coachProfileModel.HiredAt,
                coachProfileModel.StatusNotes,
                coachProfileModel.ExpectedReturnDate,
                Tags = JsonSerializer.Serialize(coachProfileModel.Tags.Select(t => t.ToString()))
            });
        }

        public async Task<CoachProfileModel?> GetCoachProfileByIdAsync(Guid accountId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"SELECT * FROM ""CoachProfiles"" WHERE ""AccountId"" = @AccountId";

            var result = await connection.QuerySingleOrDefaultAsync(sql, new
            {
                AccountId = accountId
            });

            if (result is null)
            {
                return null;
            }

            var tags = JsonSerializer.Deserialize<List<string>>((string)result.Tags)!
                .Select(tg => Enum.Parse<TagEnum>(tg)).ToList();

            return new CoachProfileModel()
            {
                AccountId = result.AccountId,
                FirstName = result.FirstName,
                LastName = result.LastName,
                DateOfBirth = result.DateOfBirth,
                Gender = result.Gender,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt,
                AccountStatus = Enum.Parse<ClientStatus>(result.AccountStatus),
                StatusNotes = result.StatusNotes,
                HiredAt = result.HiredAt,
                CostPerHour = result.CostPerHour,
                CoachStatus = Enum.Parse<CoachStatus>(result.CoachStatus),
                ExpectedReturnDate = result.ExpectedReturnDate,
                Tags = tags
            };
        }

        public async Task UpdateCoachProfileAsync(CoachProfileModel profileModel)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            UPDATE ""CoachProfiles""
            SET
                ""FirstName"" = @FirstName, ""LastName"" = @LastName,
                ""DateOfBirth"" = @DateOfBirth, ""Gender"" = @Gender,
                ""UpdatedAt"" = @UpdatedAt, ""Tags"" = @Tags,
                ""AccountStatus"" = @AccountStatus, 
                ""StatusNotes"" = @StatusNotes, ""ExpectedReturnDate"" = @ExpectedReturnDate,
                ""CoachStatus"" = @CoachStatus
            WHERE ""AccountId"" = @AccountId;";

            await connection.ExecuteAsync(sql, new
            {
                profileModel.AccountId,
                profileModel.FirstName,
                profileModel.LastName,
                profileModel.DateOfBirth,
                profileModel.Gender,
                UpdatedAt = DateTime.UtcNow,
                Tags = profileModel.Tags.Select(t => t.ToString()),
                AccountStatus = profileModel.AccountStatus.ToString(),
                profileModel.StatusNotes,
                profileModel.ExpectedReturnDate,
                CoachStatus = profileModel.CoachStatus.ToString(),
            });
        }

        public async Task RemoveCoachProfileAsync(Guid accountId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            DELETE FROM ""CoachProfiles""
            WHERE ""AccountId"" = @AccountId;";

            await connection.ExecuteAsync(sql, new
            {
                AccountId = accountId,
            });
        }

        public async Task<bool> DoesAccountExistAsync(Guid accountId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"SELECT 1 FROM ""CoachProfiles"" WHERE ""AccountId"" = @AccountId";
            var result = await connection.QuerySingleOrDefaultAsync<int?>(sql, new
            {
                AccountId = accountId
            });

            return result != null;
        }
    }
}
