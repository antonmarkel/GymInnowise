using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Logic.Services;
using GymInnowise.ReportService.Perstistence.Data;
using GymInnowise.ReportService.Perstistence.Models.Base;
using GymInnowise.ReportService.Perstistence.Reporisitories;
using GymInnowise.ReportService.Perstistence.Reporisitories.Interfaces;
using GymInnowise.Shared.Reports.Interfaces;

namespace GymInnowise.ReportService.API.Extensions
{
    public static class ApplicationExtenstion
    {
        public static void AddPersistence(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ReportServiceContext>();
        }

        public static void AddReportServices<TReport, TReportEntity>(this WebApplicationBuilder builder)
            where TReport : IReport
            where TReportEntity : ReportEntityBase
        {
            builder.Services.AddScoped<IReportRepository<TReportEntity>, ReportRepository<TReportEntity>>();
            builder.Services.AddScoped<IHtmlReportGenerator<TReport>, HtmlReportGenerator<TReport>>();
            builder.Services.AddScoped<IReportFileGenerator<TReport>, ReportFileGenerator<TReport>>();
            builder.Services.AddScoped<IReportService<TReport, TReportEntity>, ReportService<TReport, TReportEntity>>();
        }
    }
}
