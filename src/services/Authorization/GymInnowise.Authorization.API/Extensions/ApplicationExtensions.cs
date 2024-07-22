using GymInnowise.Authorization.API.Middleware;
using GymInnowise.Authorization.Configuration.Token;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Logic.Services;
using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Repositories.Implementations;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GymInnowise.Authorization.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }

        public static void AddPersistanceServices(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AuthorizationDbContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
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
            builder.Services.AddScoped<ITokenService, TokenService>();
        }
    }
}