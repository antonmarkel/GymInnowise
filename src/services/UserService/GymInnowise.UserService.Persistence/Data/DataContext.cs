using Dapper;
using GymInnowise.UserService.Configuration.Data;
using Microsoft.Extensions.Options;
using Npgsql;

namespace GymInnowise.UserService.Persistence.Data
{
    public class DataContext
    {
        private readonly DbSettings _dbSettings;

        public DataContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        public async Task EnsureDatabaseCreatedAsync()
        {
            var connectionStringWithoutDatabase = $@"
                Host={_dbSettings.Server}; 
                Port={_dbSettings.Port}; 
                Database=postgres; 
                Username={_dbSettings.UserId}; 
                Password={_dbSettings.Password};";

            await using var connection = new NpgsqlConnection(connectionStringWithoutDatabase);
            var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_dbSettings.Database}';";
            var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
            if (dbCount == 0)
            {
                var sql = $"CREATE DATABASE \"{_dbSettings.Database}\"";
                await connection.ExecuteAsync(sql);
            }
        }
    }
}