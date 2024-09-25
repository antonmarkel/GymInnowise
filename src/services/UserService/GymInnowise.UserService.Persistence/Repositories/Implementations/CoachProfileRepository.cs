using Dapper;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Enums;
using System.Text.Json;

namespace GymInnowise.UserService.Persistence.Repositories.Implementations
{
    public class CoachProfileRepository(DataContext _dataContext) : IProfileRepository<CoachProfileEntity>
    {
        public async Task CreateProfileAsync(CoachProfileEntity coachProfile)
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
                @ExpectedReturnDate, @CoachStatus, @Tags
            );";

            await connection.ExecuteAsync(sql, new
            {
                coachProfile.AccountId,
                coachProfile.FirstName,
                coachProfile.LastName,
                coachProfile.DateOfBirth,
                coachProfile.Gender,
                coachProfile.CreatedAt,
                coachProfile.UpdatedAt,
                AccountStatus = coachProfile.AccountStatus.ToString(),
                CoachStatus = coachProfile.CoachStatus.ToString(),
                coachProfile.CostPerHour,
                coachProfile.HiredAt,
                coachProfile.StatusNotes,
                coachProfile.ExpectedReturnDate,
                Tags = JsonSerializer.Serialize(coachProfile.Tags.Select(t => t.ToString()))
            });
        }

        public async Task<CoachProfileEntity?> GetProfileByIdAsync(Guid accountId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"SELECT * FROM ""CoachProfiles"" WHERE ""AccountId"" = @AccountId";

            var result = await connection.QuerySingleOrDefaultAsync(sql, new
            {
                AccountId = accountId
            });

            if (result is null)
            {
                return result;
            }

            var tagsString = (string)result.Tags;
            var tags = JsonSerializer.Deserialize<List<string>>(tagsString)!
                .Select(tg => Enum.Parse<TagEnum>(tg)).ToList();

            return new CoachProfileEntity
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

        public async Task UpdateProfileAsync(CoachProfileEntity profile)
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
                profile.AccountId,
                profile.FirstName,
                profile.LastName,
                profile.DateOfBirth,
                profile.Gender,
                UpdatedAt = DateTime.UtcNow,
                Tags = JsonSerializer.Serialize(profile.Tags.Select(t => t.ToString())),
                AccountStatus = profile.AccountStatus.ToString(),
                profile.StatusNotes,
                profile.ExpectedReturnDate,
                CoachStatus = profile.CoachStatus.ToString()
            });
        }

        public async Task<bool> DoesProfileExistAsync(Guid accountId)
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
