using GymInnowise.EmailService.Configuration.Email;
using GymInnowise.EmailService.Configuration.Templates;
using GymInnowise.EmailService.Logic.Consumers;
using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Logic.Services;
using GymInnowise.Shared.Configuration;
using GymInnowise.Shared.Configuration.Token;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GymInnowise.EmailService.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
            builder.Services.AddScoped<IEmailService, Logic.Services.EmailService>();
            builder.Services.AddScoped<IMessageBuilder, MessageBuilder>();
        }

        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));
            builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)));
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
            builder.Services.Configure<TemplateSettings>(builder.Configuration.GetSection(nameof(TemplateSettings)));
        }

        public static void AddRabbitMq(this WebApplicationBuilder builder)
        {
            var rabbitMqSettings = builder.Configuration.GetSection("RabbitMqSettings");
            builder.Services.AddMassTransit(busConfig =>
            {
                busConfig.SetKebabCaseEndpointNameFormatter();
                busConfig.AddConsumer<SendMessageConsumer>();
                busConfig.AddConsumer<SendTemplateMessageConsumer>();
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
    }
}