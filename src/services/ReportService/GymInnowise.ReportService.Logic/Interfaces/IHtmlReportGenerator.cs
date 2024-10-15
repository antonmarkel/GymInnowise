using GymInnowise.Shared.Reports.Interfaces;

namespace GymInnowise.ReportService.Logic.Interfaces
{
    public interface IHtmlReportGenerator<in TReport> where TReport : IReport
    {
        Task<string> GenerateHtmlAsync(TReport report);
    }
}
