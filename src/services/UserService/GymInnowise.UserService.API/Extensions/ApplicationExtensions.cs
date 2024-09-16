﻿using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using GymInnowise.UserService.API.Authorization;
using GymInnowise.UserService.API.Validators.Creates;
using GymInnowise.UserService.Configuration.Token;
using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Logic.Services;
using GymInnowise.UserService.Persistence.Data;
using GymInnowise.UserService.Persistence.Migrations;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Implementations;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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

            builder.Services.AddScoped<IProfileRepository<ClientProfileEntity>, ClientProfileRepository>();
            builder.Services.AddScoped<IProfileRepository<CoachProfileEntity>, CoachProfileRepository>();
            builder.Services.AddScoped<IPersonalGoalRepository, PersonalGoalRepository>();
        }

        public static void AddUserServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IClientProfileService, ClientProfileService>();
            builder.Services.AddScoped<ICoachProfileService, CoachProfileService>();
            builder.Services.AddScoped<IPersonalGoalService, PersonalGoalService>();
        }

        public static void AddAutherizationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IAuthorizationHandler, OwnerOrAdminHandler>();
        }

        public static void AddValidation(this IHostApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateClientProfileRequestValidator>();
        }

        public static void AddJwtServices(this IHostApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(jwtSettings);
            var key = Encoding.ASCII.GetBytes(jwtSettings.Get<JwtSettings>()!.SecretKey);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    RoleClaimType = "roles"
                };
            });
            builder.Services.AddAuthorization();
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