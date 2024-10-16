using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Logic.Results;
using GymInnowise.Shared.Reports.Interfaces;
using OneOf;

namespace GymInnowise.ReportService.Logic.Services
{
    public class ReportFileGenerator<TReport> : IReportFileGenerator<TReport> where TReport : IReport
    {
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IHtmlReportGenerator<TReport> _htmlGenerator;

        public ReportFileGenerator(IPdfGenerator pdfGenerator, IHtmlReportGenerator<TReport> htmlGenerator)
        {
            _pdfGenerator = pdfGenerator;
            _htmlGenerator = htmlGenerator;
        }

        public async Task<OneOf<Stream, HtmlGenerationFailed, PdfGenerationFailed>> GenerateReportAsync(TReport report)
        {
            var htmlGenerationResult = await _htmlGenerator.GenerateHtmlAsync(report);
            if (htmlGenerationResult.IsT1)
            {
                return new HtmlGenerationFailed();
            }

            var pdfGenerationResult = await _pdfGenerator.GeneratePdfFromHtmlAsync(htmlGenerationResult.AsT0);

            if (pdfGenerationResult.IsT1)
            {
                return new PdfGenerationFailed();
            }

            var stream = pdfGenerationResult.AsT0;
            stream.Position = 0;

            return stream;
        }
    }
}
