using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Logic.Results;
using GymInnowise.Shared.Reports.Interfaces;
using Microsoft.Extensions.Logging;
using OneOf;

namespace GymInnowise.ReportService.Logic.Services
{
    public class ReportFileGenerator<TReport> : IReportFileGenerator<TReport> where TReport : IReport
    {
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IHtmlReportGenerator<TReport> _htmlGenerator;
        private readonly ILogger<ReportFileGenerator<TReport>> _logger;

        public ReportFileGenerator(IPdfGenerator pdfGenerator, IHtmlReportGenerator<TReport> htmlGenerator,
            ILogger<ReportFileGenerator<TReport>> logger)
        {
            _pdfGenerator = pdfGenerator;
            _htmlGenerator = htmlGenerator;
            _logger = logger;
        }

        public async Task<OneOf<Stream, HtmlGenerationFailed, PdfGenerationFailed>> GenerateReportAsync(TReport report)
        {
            var htmlGenerationResult = await _htmlGenerator.GenerateHtmlAsync(report);
            if (htmlGenerationResult.IsT1)
            {
                _logger.LogWarning("Html generation failed!");

                return new HtmlGenerationFailed();
            }

            var pdfGenerationResult = await _pdfGenerator.GeneratePdfFromHtmlAsync(htmlGenerationResult.AsT0);

            if (pdfGenerationResult.IsT1)
            {
                _logger.LogWarning("Pdf generation failed");

                return new PdfGenerationFailed();
            }

            var stream = pdfGenerationResult.AsT0;
            stream.Position = 0;
            _logger.LogInformation("Pdf report was successfully generated: @{report}", report);

            return stream;
        }
    }
}
