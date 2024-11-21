namespace GymInnowise.FileService.Persistence.Services.Interfaces
{
    public interface IBlobService
    {
        Task<Stream?> DownloadAsync(string fileId, string container,
            CancellationToken cancellationToken = default);

        Task UploadAsync(Stream stream, string contentType, string fileId, string container,
            CancellationToken cancellationToken = default);
    }
}
