using GymInnowise.ReportService.API.Middleware;
using GymInnowise.ReportService.Configuration.Settings;
using GymInnowise.ReportService.Logic.Consumers;
using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Logic.Mappings.Base;
using GymInnowise.ReportService.Logic.Services;
using GymInnowise.ReportService.Perstistence.Data;
using GymInnowise.ReportService.Perstistence.Models.Entities;
using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.ReportService.Perstistence.Reporisitories;
using GymInnowise.ReportService.Perstistence.Reporisitories.Interfaces;
using GymInnowise.Shared.Configuration.Token;
using GymInnowise.Shared.Reports.Interfaces;
using GymInnowise.Shared.Reports.Payment;
using GymInnowise.Shared.Reports.Trainings;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using System.Text;

namespace GymInnowise.ReportService.API.Extensions
{
    public static class ApplicationExtenstion
    {
        public static void AddPersistence(this WebApplicationBuilder builder)
        {
            var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

            builder.Services.AddDbContext<ReportServiceContext>(options =>
                options.UseMongoDB(mongoDbSettings!.DefaultConnection, mongoDbSettings.DefaultDatabaseName));
        }

        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ReportViewSettings>(
                builder.Configuration.GetSection("ReportViewSettings"));
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPdfGenerator, PdfGenerator>();
            builder.Services.AddAutoMapper(typeof(ReportMappingProfileBase<,>).Assembly);
        }

        public static void AddReportServices<TReport, TReportEntity>(this WebApplicationBuilder builder)
            where TReport : IReport
            where TReportEntity : class, TReport, IReportEntity
        {
            builder.Services.AddScoped<IReportRepository<TReportEntity>, ReportRepository<TReportEntity>>();
            builder.Services.AddScoped<IHtmlReportGenerator<TReport>, HtmlReportGenerator<TReport>>();
            builder.Services.AddScoped<IReportFileGenerator<TReport>, ReportFileGenerator<TReport>>();
            builder.Services.AddScoped<IReportService<TReport, TReportEntity>, ReportService<TReport, TReportEntity>>();
        }

        public static void AddReports(this WebApplicationBuilder builder)
        {
            builder.AddReportServices<GroupTrainingReport, GroupTrainingReportEntity>();
            builder.AddReportServices<IndividualTrainingReport, IndividualTrainingReportEntity>();
            builder.AddReportServices<IndividualWithCoachTrainingReport, IndividualWithCoachTrainingReportEntity>();
            builder.AddReportServices<BillReport, BillReportEntity>();
        }

        public static void AddRabbitMq(this WebApplicationBuilder builder)
        {
            var rabbitMqSettings = builder.Configuration.GetSection("RabbitMqSettings");
            builder.Services.AddMassTransit(busConfig =>
            {
                busConfig.SetKebabCaseEndpointNameFormatter();
                busConfig.AddConsumer<ReportConsumer<GroupTrainingReport, GroupTrainingReportEntity>>();
                busConfig.AddConsumer
                    <ReportConsumer<IndividualWithCoachTrainingReport, IndividualWithCoachTrainingReportEntity>>();
                busConfig.AddConsumer<ReportConsumer<IndividualTrainingReport, IndividualTrainingReportEntity>>();
                busConfig.AddConsumer<ReportConsumer<BillReport, BillReportEntity>>();
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

        public static void AddLogger(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration((builder.Configuration))
                .CreateLogger();

            builder.Services.AddSerilog(Log.Logger);
        }

        public static void UseGlobalExceptionHandler(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
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