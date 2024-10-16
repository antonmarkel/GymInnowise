using AutoMapper;
using GymInnowise.ReportService.Configuration.Settings;
using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Logic.Mappings;
using GymInnowise.ReportService.Logic.Mappings.Base;
using GymInnowise.ReportService.Logic.Services;
using GymInnowise.ReportService.Perstistence.Data;
using GymInnowise.ReportService.Perstistence.Models.Entities;
using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.ReportService.Perstistence.Reporisitories;
using GymInnowise.ReportService.Perstistence.Reporisitories.Interfaces;
using GymInnowise.Shared.Reports;
using GymInnowise.Shared.Reports.Interfaces;
using Microsoft.EntityFrameworkCore;

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
    }
}