using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Logic.Results;
using iText.Html2pdf;
using Microsoft.Extensions.Logging;
using OneOf;

namespace GymInnowise.ReportService.Logic.Services
{
    public class PdfGenerator : IPdfGenerator
    {
        private readonly ILogger<PdfGenerator> _logger;

        public PdfGenerator(ILogger<PdfGenerator> logger)
        {
            _logger = logger;
        }

        public async Task<OneOf<Stream, PdfGenerationFailed>> GeneratePdfFromHtmlAsync(string htmlReport,
            CancellationToken cancellationToken = default)
        {
            var stream = new MemoryStream();
            var generationTask = Task.Run(() => HtmlConverter.ConvertToPdf(htmlReport, stream), cancellationToken);
            var isFailed = false;

            await generationTask.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    isFailed = true;
                }
            }, cancellationToken);

            if (isFailed)
            {
                _logger.LogError("Pdf convert error!");

                return new PdfGenerationFailed();
            }

            _logger.LogInformation("Pdf was successfully generated");

            return new MemoryStream(stream.ToArray());
        }
    }
}
