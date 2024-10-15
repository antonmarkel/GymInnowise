using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Logic.Results;
using iText.Html2pdf;
using OneOf;

namespace GymInnowise.ReportService.Logic.Services
{
    public class PdfGenerator : IPdfGenerator
    {
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
                return new PdfGenerationFailed();
            }

            return new MemoryStream(stream.ToArray());
        }
    }
}
