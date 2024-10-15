using GymInnowise.ReportService.Logic.Results;
using GymInnowise.Shared.Reports.Interfaces;
using OneOf;

namespace GymInnowise.ReportService.Logic.Interfaces
{
    public interface IReportFileGenerator<TReport> where TReport : IReport
    {
        Task<OneOf<Stream, HtmlGenerationFailed, PdfGenerationFailed>> GenerateReportAsync(TReport report);
    }
}
