using FluentValidation;
using FluentValidation.AspNetCore;
using GymInnowise.GymService.API.Authorization;
using GymInnowise.GymService.API.Authorization.Handlers;
using GymInnowise.GymService.API.Authorization.Requirements;
using GymInnowise.GymService.API.Validators.Base;
using GymInnowise.GymService.Configuration;
using GymInnowise.GymService.Logic.Interfaces;
using GymInnowise.GymService.Logic.Services;
using GymInnowise.GymService.Persistence.Data;
using GymInnowise.GymService.Persistence.Repositories.Implementations;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace GymInnowise.GymService.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddGymServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IGymService, Logic.Services.GymService>();
            builder.Services.AddScoped<IGymEventService, GymEventService>();
        }

        public static void AddPersistenceServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<GymServiceDbContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddScoped<IGymRepository, GymRepository>();
            builder.Services.AddScoped<IGymEventRepository, GymEventRepository>();
        }

        public static void AddValidationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(GymDetailsBaseDtoValidator)));
        }

        public static void AddAuthorizationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.GymManagerPolicy,
                    policy => policy.Requirements.Add(new GymManagerRequirement()));
            });
            builder.Services.AddSingleton<IAuthorizationHandler, GymManagerHandler>();
        }

        public static void AddLogger(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration((builder.Configuration))
                .CreateLogger();

            builder.Services.AddSerilog(Log.Logger);
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
                    RoleClaimType = ClaimTypes.Role
                };
            });
        }
    }
}
