using GymInnowise.ReportService.Logic.Results;
using OneOf;

namespace GymInnowise.ReportService.Logic.Interfaces
{
    public interface IPdfGenerator
    {
        Task<OneOf<Stream, PdfGenerationFailed>> GeneratePdfFromHtmlAsync(string htmlReport,
            CancellationToken cancellationToken = default);
    }
}
