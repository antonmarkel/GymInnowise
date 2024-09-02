﻿using Dapper;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels;
using System.Reflection;
using System.Text.Json;
using GymInnowise.UserService.Shared.Enums;

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

            return new ClientProfileModel()
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
                ExpectedReturnDate = result.ExpectedReturnDate,
                Tags = tags
            };
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

        public async Task UpdateProfileStatusAsync(UpdateClientProfileStatusRequest updateProfileStatusRequest)
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
