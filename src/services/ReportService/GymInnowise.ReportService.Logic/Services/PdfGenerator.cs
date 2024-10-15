using GymInnowise.ReportService.Logic.Interfaces;
using iText.Html2pdf;

namespace GymInnowise.ReportService.Logic.Services
{
    public class PdfGenerator : IPdfGenerator
    {
        public async Task<Stream> GeneratePdfFromHtmlAsync(string htmlReport,
            CancellationToken cancellationToken = default)
        {
            var stream = new MemoryStream();
            await Task.Run(() => HtmlConverter.ConvertToPdf(htmlReport, stream), cancellationToken);

            return new MemoryStream(stream.ToArray());
        }
    }
}
