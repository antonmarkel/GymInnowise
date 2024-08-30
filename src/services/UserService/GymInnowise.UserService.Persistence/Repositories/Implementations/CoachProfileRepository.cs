using Dapper;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels;
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

            return await connection.QuerySingleOrDefaultAsync<CoachProfileModel?>(sql,
                new { AccountId = accountId });
        }

        public async Task UpdateCoachProfileAsync(UpdateCoachProfileRequest updateCoachProfileRequest)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            UPDATE ""CoachProfiles""
            SET
                ""FirstName"" = @FirstName, ""LastName"" = @LastName,
                ""DateOfBirth"" = @DateOfBirth, ""Gender"" = @Gender,
                ""UpdatedAt"" = @UpdatedAt, ""Tags"" = @Tags
            WHERE ""AccountId"" = @AccountId;";

            await connection.ExecuteAsync(sql, new
            {
                updateCoachProfileRequest.AccountId,
                updateCoachProfileRequest.FirstName,
                updateCoachProfileRequest.LastName,
                updateCoachProfileRequest.DateOfBirth,
                updateCoachProfileRequest.Gender,
                UpdatedAt = DateTime.UtcNow,
                Tags = updateCoachProfileRequest.Tags.Select(t => t.ToString()),
            });
        }

        public async Task UpdateProfileStatusAsync(UpdateCoachProfileStatusRequest updateCoachProfileStatusRequest)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            UPDATE ""CoachProfiles""
            SET
                ""AccountStatus"" = @AccountStatus, ""StatusNotes"" = @StatusNotes,
                ""ExpectedReturnDate"" = @ExpectedReturnDate,
                ""CoachStatus"" = @CoachStatus
            WHERE ""AccountId"" = @AccountId;";

            await connection.ExecuteAsync(sql, new
            {
                updateCoachProfileStatusRequest.AccountId,
                AccountStatus = updateCoachProfileStatusRequest.AccountStatus.ToString(),
                updateCoachProfileStatusRequest.StatusNotes,
                updateCoachProfileStatusRequest.ExpectedReturnDate,
                CoachStatus = updateCoachProfileStatusRequest.CoachStatus.ToString(),
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
    }
}
