using FluentMigrator.Runner;
using GymInnowise.UserService.Configuration.Data;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Migrations;
using System.Reflection;

namespace GymInnowise.UserService.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddPersistenceServices(this IHostApplicationBuilder builder)
        {
            var dbSettingsSection = builder.Configuration.GetSection("DbSettings");
            builder.Services.Configure<DbSettings>(dbSettingsSection);
            var dbSettings = dbSettingsSection.Get<DbSettings>();

            builder.Services.AddSingleton<DataContext>();
            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddPostgres()
                    .WithGlobalConnectionString(dbSettings!.GetConnectionString())
                    .ScanIn(Assembly.GetAssembly(typeof(InitialCreate))).For.Migrations());
        }

        public static async Task MigrateDatabaseAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<DataContext>();
                await databaseService.EnsureDatabaseCreatedAsync();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                migrationService.ListMigrations();
                migrationService.MigrateUp();
            }
        }
    }
}