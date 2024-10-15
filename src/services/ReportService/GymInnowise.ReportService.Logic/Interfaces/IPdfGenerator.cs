namespace GymInnowise.ReportService.Logic.Interfaces
{
    public interface IPdfGenerator
    {
        Task<Stream> GeneratePdfFromHtmlAsync(string htmlReport, CancellationToken cancellationToken = default);
    }
}
