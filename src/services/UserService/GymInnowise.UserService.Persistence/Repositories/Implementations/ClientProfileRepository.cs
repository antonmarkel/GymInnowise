using Dapper;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels;
using System.Text.Json;

namespace GymInnowise.UserService.Persistence.Repositories.Implementations
{
    public class ClientProfileRepository(DataContext _dataContext) : IClientProfileRepository
    {
        public async Task CreateClientProfileAsync(ClientProfileModel clientProfileModel)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            INSERT INTO ""ClientProfiles""
            (
                ""AccountId"", ""FirstName"", ""LastName"", ""DateOfBirth"", ""Gender"", 
                ""CreatedAt"", ""UpdatedAt"", ""AccountStatus"", ""StatusNotes"", 
                ""ExpectedReturnDate"", ""Tags""
            )
            VALUES
            (
                @AccountId, @FirstName, @LastName, @DateOfBirth, @Gender, 
                @CreatedAt, @UpdatedAt, @AccountStatus, @StatusNotes, 
                @ExpectedReturnDate, @Tags
            );";

            await connection.ExecuteAsync(sql, new
            {
                AccountId = Guid.NewGuid(),
                clientProfileModel.FirstName,
                clientProfileModel.LastName,
                clientProfileModel.DateOfBirth,
                clientProfileModel.Gender,
                clientProfileModel.CreatedAt,
                clientProfileModel.UpdatedAt,
                AccountStatus = clientProfileModel.AccountStatus.ToString(),
                clientProfileModel.StatusNotes,
                clientProfileModel.ExpectedReturnDate,
                Tags = JsonSerializer.Serialize(clientProfileModel.Tags.Select(t => t.ToString()))
            });
        }

        public async Task<ClientProfileModel?> GetClientProfileByIdAsync(Guid accountId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"SELECT * FROM ""ClientProfiles"" WHERE ""AccountId"" = @AccountId";

            return await connection.QuerySingleOrDefaultAsync<ClientProfileModel?>(sql,
                new { AccountId = accountId });
        }

        public async Task UpdateClientProfileAsync(UpdateClientProfileRequest updateClientProfileRequest)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            UPDATE ""ClientProfiles""
            SET
                ""FirstName"" = @FirstName, ""LastName"" = @LastName,
                ""DateOfBirth"" = @DateOfBirth, ""Gender"" = @Gender,
                ""UpdatedAt"" = @UpdatedAt, ""Tags"" = @Tags
            WHERE ""AccountId"" = @AccountId;";

            await connection.ExecuteAsync(sql, new
            {
                updateClientProfileRequest.AccountId,
                updateClientProfileRequest.FirstName,
                updateClientProfileRequest.LastName,
                updateClientProfileRequest.DateOfBirth,
                updateClientProfileRequest.Gender,
                UpdatedAt = DateTime.UtcNow,
                Tags = updateClientProfileRequest.Tags.Select(t => t.ToString()),
            });
        }

        public async Task UpdateProfileStatusAsync(UpdateProfileStatusRequest updateProfileStatusRequest)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            UPDATE ""ClientProfiles""
            SET
                ""AccountStatus"" = @AccountStatus, ""StatusNotes"" = @StatusNotes,
                ""ExpectedReturnDate"" = @ExpectedReturnDate
            WHERE ""AccountId"" = @AccountId;";

            await connection.ExecuteAsync(sql, new
            {
                updateProfileStatusRequest.AccountId,
                AccountStatus = updateProfileStatusRequest.AccountStatus.ToString(),
                updateProfileStatusRequest.StatusNotes,
                updateProfileStatusRequest.ExpectedReturnDate,
            });
        }

        public async Task RemoveClientProfileAsync(Guid accountId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            DELETE FROM ""ClientProfiles""
            WHERE ""AccountId"" = @AccountId;";

            await connection.ExecuteAsync(sql, new
            {
                AccountId = accountId,
            });
        }
    }
}
