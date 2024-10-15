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
            var html = htmlGenerationResult.AsT0;
            if (html is null)
            {
                return new HtmlGenerationFailed();
            }

            var pdfGenerationResult = await _pdfGenerator.GeneratePdfFromHtmlAsync(html);
            var pdfStream = pdfGenerationResult.AsT0;
            if (pdfStream is null)
            {
                return new PdfGenerationFailed();
            }

            return pdfStream;
        }
    }
}
