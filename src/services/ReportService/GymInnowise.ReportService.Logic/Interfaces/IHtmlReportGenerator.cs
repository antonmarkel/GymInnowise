using GymInnowise.ReportService.Logic.Results;
using GymInnowise.Shared.Reports.Interfaces;
using OneOf;

namespace GymInnowise.ReportService.Logic.Interfaces
{
    public interface IHtmlReportGenerator<in TReport> where TReport : IReport
    {
        Task<OneOf<string, HtmlGenerationFailed>> GenerateHtmlAsync(TReport report);
    }
}
