using Dapper;
using GymInnowise.UserService.Configuration.Data;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;


namespace GymInnowise.UserService.Persistence.Data
{
    public class DataContext
    {
        private DbSettings _dbSettings;

        public DataContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
            EnsureCreatedAsync().Wait();
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = $@"
                Host={_dbSettings.Server}; 
                Port={_dbSettings.Port}; 
                Database={_dbSettings.Database}; 
                Username={_dbSettings.UserId}; 
                Password={_dbSettings.Password};";

            return new NpgsqlConnection(connectionString);
        }

        public async Task EnsureCreatedAsync()
        {
            await EnsureDatabaseCreatedAsync();
            await EnsureTablesCreatedAsync();
        }

        private async Task EnsureDatabaseCreatedAsync()
        {
            var connectionString = $@"
                Host={_dbSettings.Server}; 
                Port={_dbSettings.Port}; 
                Database=postgres; 
                Username={_dbSettings.UserId}; 
                Password={_dbSettings.Password};";

            using var connection = new NpgsqlConnection(connectionString);
            var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_dbSettings.Database}';";
            var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
            if (dbCount == 0)
            {
                var sql = $"CREATE DATABASE \"{_dbSettings.Database}\"";
                await connection.ExecuteAsync(sql);
            }
        }

        private async Task EnsureTablesCreatedAsync()
        {
            var connection = CreateConnection();
            await EnsureClientProfilesTableCreatedAsync(connection);
            await EnsureCoachProfilesTableCreatedAsync(connection);
            await EnsurePersonalGoalsTableCreatedAsync(connection);
        }

        private async Task EnsureClientProfilesTableCreatedAsync(IDbConnection connection)
        {
            var sql = """
                          CREATE TABLE IF NOT EXISTS ClientProfiles (
                              AccountId UUID PRIMARY KEY,
                              FirstName VARCHAR(50),
                              LastName VARCHAR(50),
                              DateOfBirth TIMESTAMP WITH TIME ZONE,
                              Gender VARCHAR(50),
                              CreatedAt TIMESTAMP WITH TIME ZONE,
                              UpdatedAt TIMESTAMP WITH TIME ZONE,
                              AccountStatus VARCHAR(50),
                              StatusNotes VARCHAR(1000),
                              ExpectedReturnDate TIMESTAMP WITH TIME ZONE,
                              Tags VARCHAR
                          );
                      """;
            await connection.ExecuteAsync(sql);
        }

        private async Task EnsureCoachProfilesTableCreatedAsync(IDbConnection connection)
        {
            var sql = """
                          CREATE TABLE IF NOT EXISTS CoachProfiles (
                              AccountId UUID PRIMARY KEY,
                              FirstName VARCHAR(50),
                              LastName VARCHAR(50),
                              DateOfBirth TIMESTAMP WITH TIME ZONE,
                              Gender VARCHAR(50),
                              CreatedAt TIMESTAMP WITH TIME ZONE,
                              UpdatedAt TIMESTAMP WITH TIME ZONE,
                              HiredAt TIMESTAMP WITH TIME ZONE,
                              CostPerHour DECIMAL,
                              AccountStatus VARCHAR(50),
                              StatusNotes VARCHAR(1000),
                              ExpectedReturnDate TIMESTAMP WITH TIME ZONE,
                              CoachStatus VARCHAR(50),
                              Tags VARCHAR
                          );
                      """;
            await connection.ExecuteAsync(sql);
        }

        private async Task EnsurePersonalGoalsTableCreatedAsync(IDbConnection connection)
        {
            var sql = """
                          CREATE TABLE IF NOT EXISTS PersonalGoals (
                              Id UUID PRIMARY KEY,
                              Owner UUID,
                              Goal VARCHAR(200),
                              SupervisorCoach UUID,
                              Status VARCHAR(50),
                              StartDate TIMESTAMP WITH TIME ZONE,
                              DeadLine TIMESTAMP WITH TIME ZONE
                          );
                      """;
            await connection.ExecuteAsync(sql);
        }
    }
}