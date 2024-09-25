using Dapper;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Enums;
using System.Text.Json;

namespace GymInnowise.UserService.Persistence.Repositories.Implementations
{
    public class ClientProfileRepository(DataContext _dataContext) : IProfileRepository<ClientProfileEntity>
    {
        public async Task CreateProfileAsync(ClientProfileEntity clientProfile)
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
                clientProfile.AccountId,
                clientProfile.FirstName,
                clientProfile.LastName,
                clientProfile.DateOfBirth,
                clientProfile.Gender,
                clientProfile.CreatedAt,
                clientProfile.UpdatedAt,
                AccountStatus = clientProfile.AccountStatus.ToString(),
                clientProfile.StatusNotes,
                clientProfile.ExpectedReturnDate,
                Tags = JsonSerializer.Serialize(clientProfile.Tags.Select(t => t.ToString()))
            });
        }

        public async Task<ClientProfileEntity?> GetProfileByIdAsync(Guid accountId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"SELECT * FROM ""ClientProfiles"" WHERE ""AccountId"" = @AccountId";

            var result = await connection.QuerySingleOrDefaultAsync(sql, new
            {
                AccountId = accountId
            });

            if (result is null)
            {
                return result;
            }

            var tags = JsonSerializer.Deserialize<List<string>>((string)result.Tags)!
                .Select(tg => Enum.Parse<TagEnum>(tg)).ToList();

            return new ClientProfileEntity()
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

        public async Task UpdateProfileAsync(ClientProfileEntity profile)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"
            UPDATE ""ClientProfiles""
            SET
                ""FirstName"" = @FirstName, ""LastName"" = @LastName,
                ""DateOfBirth"" = @DateOfBirth, ""Gender"" = @Gender,
                ""UpdatedAt"" = @UpdatedAt, ""Tags"" = @Tags,
                ""AccountStatus"" = @AccountStatus, ""StatusNotes"" = @StatusNotes,
                ""ExpectedReturnDate"" = @ExpectedReturnDate
            WHERE ""AccountId"" = @AccountId;";

            await connection.ExecuteAsync(sql, new
            {
                profile.AccountId,
                profile.FirstName,
                profile.LastName,
                profile.DateOfBirth,
                profile.Gender,
                UpdatedAt = DateTime.UtcNow,
                AccountStatus = profile.AccountStatus.ToString(),
                profile.StatusNotes,
                profile.ExpectedReturnDate,
                Tags = JsonSerializer.Serialize(profile.Tags.Select(t => t.ToString())),
            });
        }

        public async Task<bool> DoesProfileExistAsync(Guid accountId)
        {
            using var connection = _dataContext.CreateConnection();
            const string sql = @"SELECT 1 FROM ""ClientProfiles"" WHERE ""AccountId"" = @AccountId";
            var result = await connection.QuerySingleOrDefaultAsync<int?>(sql, new
            {
                AccountId = accountId
            });

            return result != null;
        }
    }
}