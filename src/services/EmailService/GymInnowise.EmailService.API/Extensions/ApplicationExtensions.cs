using FluentValidation;
using FluentValidation.AspNetCore;
using GymInnowise.Authorization.Configuration.Token;
using GymInnowise.EmailService.API.Services.Implementations;
using GymInnowise.EmailService.API.Services.Interfaces;
using GymInnowise.EmailService.API.Validators;
using GymInnowise.EmailService.Configuration.Email;
using GymInnowise.EmailService.Logic.Features.Accounts;
using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Logic.Services;
using GymInnowise.EmailService.Persistence.Data;
using GymInnowise.EmailService.Persistence.Repositories.Implementations;
using GymInnowise.EmailService.Persistence.Repositories.Interfaces;
using GymInnowise.EmailService.Shared.Configuration;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace GymInnowise.EmailService.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddPersistenceService(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<EmailServiceContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();
            builder.Services.AddScoped<IEmailVerificationRepository, EmailVerificationRepository>();
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
            builder.Services.AddScoped<ITemplateService, TemplateService>();
            builder.Services.AddScoped<IEmailService, Logic.Services.EmailService>();
            builder.Services.AddScoped<IVerificationService, VerificationService>();
            builder.Services.AddScoped<ILinkFactory, VerificationLinkFactory>();
        }

        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            var emailSettings = builder.Configuration.GetSection("EmailSettings");
            builder.Services.Configure<EmailSettings>(emailSettings);
            var rabbitMqSettings = builder.Configuration.GetSection("RabbitMqSettings");
            builder.Services.Configure<RabbitMqSettings>(rabbitMqSettings);
            var verificationSettings = builder.Configuration.GetSection("VerificationSettings");
            builder.Services.Configure<VerificationSettings>(verificationSettings);
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(jwtSettings);
        }

        public static void AddRabbitMq(this WebApplicationBuilder builder)
        {
            var rabbitMqSettings = builder.Configuration.GetSection("RabbitMqSettings");
            builder.Services.AddMassTransit(busConfig =>
            {
                busConfig.SetKebabCaseEndpointNameFormatter();
                busConfig.AddConsumer<AccountCreatedConsumer>();
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

        public static void AddJwtServices(this IHostApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
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
        }

        public static void AddValidation(this WebApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(CreateTemplateRequestValidator)));
        }
    }
}
