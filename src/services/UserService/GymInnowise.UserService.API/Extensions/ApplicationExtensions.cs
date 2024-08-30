﻿using FluentMigrator.Runner;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Migrations;
using System.Reflection;
using GymInnowise.UserService.Persistence.Repositories.Implementations;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;

namespace GymInnowise.UserService.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddPersistenceServices(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddSingleton(new DataContext(connectionString!));
            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetAssembly(typeof(InitialCreate))).For.Migrations());

            builder.Services.AddScoped<IClientProfileRepository, ClientProfileRepository>();
            builder.Services.AddScoped<ICoachProfileRepository, CoachProfileRepository>();
        }

        public static async Task MigrateDatabaseAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var databaseService = scope.ServiceProvider.GetRequiredService<DataContext>();
            await databaseService.EnsureDatabaseCreatedAsync();

            var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            migrationService.ListMigrations();
            migrationService.MigrateUp();
        }
    }
}