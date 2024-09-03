﻿using FluentMigrator.Runner;
using FluentValidation.AspNetCore;
using FluentValidation;
using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Logic.Services;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Migrations;
using GymInnowise.UserService.Persistence.Repositories.Implementations;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using System.Reflection;
using GymInnowise.UserService.API.Validators;

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
            builder.Services.AddScoped<IPersonalGoalRepository, PersonalGoalRepository>();
        }

        public static void AddUserServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IClientProfileService, ClientProfileService>();
            builder.Services.AddScoped<ICoachProfileService, CoachProfileService>();
            builder.Services.AddScoped<IPersonalGoalService, PersonalGoalService>();
        }

        public static void AddValidation(this IHostApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateClientProfileRequestValidator>();
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