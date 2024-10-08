using GymInnowise.EmailService.Configuration.Email;
using GymInnowise.EmailService.Logic.Features.Accounts;
using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Logic.Services;
using GymInnowise.EmailService.Persistence.Data;
using GymInnowise.EmailService.Persistence.Repositories.Implementations;
using GymInnowise.EmailService.Persistence.Repositories.Interfaces;
using GymInnowise.EmailService.Shared.Configuration;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.EmailService.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddPersistenceService(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<EmailServiceContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
            builder.Services.AddScoped<ITemplateService, TemplateService>();
            builder.Services.AddScoped<IEmailService, Logic.Services.EmailService>();
        }

        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("EmailSettings");
            builder.Services.Configure<EmailSettings>(jwtSettings);
        }

        public static void AddRabbitMq(this WebApplicationBuilder builder)
        {
            var rabbitMqSettings = builder.Configuration.GetSection("RabbitMq");
            builder.Services.Configure<RabbitMqSettings>(rabbitMqSettings);
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
    }
}
