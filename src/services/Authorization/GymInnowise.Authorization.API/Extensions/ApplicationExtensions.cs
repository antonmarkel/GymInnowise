using FluentValidation;
using GymInnowise.Authorization.API.Middleware;
using GymInnowise.Authorization.API.Validators;
using GymInnowise.Authorization.Configuration;
using GymInnowise.Authorization.Configuration.Token;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Logic.Services;
using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Repositories.Implementations;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Text;

namespace GymInnowise.Authorization.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void UseGlobalExceptionHandler(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }

        public static void AddPersistenceServices(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AuthorizationDbContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IVerificationRepository, VerificationRepository>();
        }

        public static void AddRabbitMq(this WebApplicationBuilder builder)
        {
            var rabbitMqSettings = builder.Configuration.GetSection("RabbitMqSettings");
            builder.Services.AddMassTransit(busConfig =>
            {
                busConfig.SetKebabCaseEndpointNameFormatter();
                busConfig.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(rabbitMqSettings["Host"]!), h =>
                    {
                        h.Username(rabbitMqSettings["Username"]!);
                        h.Password(rabbitMqSettings["Password"]!);
                    });
                    configurator.ConfigureEndpoints(context);
                });
            });
        }

        public static void AddVerificationService(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<VerificationSettings>(
                builder.Configuration.GetSection(nameof(VerificationSettings)));
            builder.Services.AddScoped<IVerificationService, VerificationService>();
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
                };
            });
            builder.Services.AddAuthorization();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
        }

        public static void AddValidation(this IHostApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        }

        public static void AddLogger(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration((builder.Configuration))
                .CreateLogger();

            builder.Services.AddSerilog(Log.Logger);
        }
    }
}