using System.Security.Claims;
using GymInnowise.SectionService.Configuration;
using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Configuration.Token;
using Microsoft.EntityFrameworkCore;
using System.Text;
using GymInnowise.SectionService.Persistence.Repositories.Cached;
using GymInnowise.SectionService.Persistence.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GymInnowise.SectionService.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static WebApplicationBuilder AddBase(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }

        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SectionDbContext>(options => options.UseNpgsql(connectionString));
            builder.Services.Scan(scan => scan
                .FromAssembliesOf(typeof(IRedundantRepository<>))
                .AddClasses(classes => classes.AssignableTo(typeof(IRedundantRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ISectionRelationRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<ISectionRepository, SectionRepository>();
            builder.Services.Decorate<ISectionRepository, SectionCachedRepository>();

            return builder;
        }

        public static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
        {
            builder.Services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMapper<,>))
                .AddClasses(classes => classes.AssignableTo(typeof(IMapper<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return builder;
        }

        public static WebApplicationBuilder AddJwtServices(this WebApplicationBuilder builder)
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
            builder.Services.AddAuthorization();

            return builder;
        }

        public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

            return builder;
        }
    }
}
