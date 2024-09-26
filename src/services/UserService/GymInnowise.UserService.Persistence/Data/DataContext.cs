using Dapper;
using Npgsql;
using System.Data;

namespace GymInnowise.UserService.Persistence.Data
{
    public class DataContext(string connectionString)
    {
        public async Task EnsureDatabaseCreatedAsync()
        {
            var database = new NpgsqlConnectionStringBuilder(connectionString).Database;
            var primaryDbConnectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
            { Database = "postgres" };

            await using var primaryConnection = new NpgsqlConnection(primaryDbConnectionStringBuilder.ToString());
            var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{database}';";
            var dbCount = await primaryConnection.ExecuteScalarAsync<int>(sqlDbCount);
            if (dbCount == 0)
            {
                var sql = $"CREATE DATABASE \"{database}\"";
                await primaryConnection.ExecuteAsync(sql);
            }
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}