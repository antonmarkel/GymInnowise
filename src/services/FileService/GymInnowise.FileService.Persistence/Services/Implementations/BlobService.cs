using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using GymInnowise.FileService.Persistence.Services.Interfaces;

namespace GymInnowise.FileService.Persistence.Services.Implementations
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<Stream?> DownloadAsync(string fileId, string container,
            CancellationToken cancellationToken = default)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(container);
            var blobClient = containerClient.GetBlobClient(fileId);
            var blobExists = await blobClient.ExistsAsync(cancellationToken);
            if (!blobExists.Value)
            {
                return null;
            }

            BlobDownloadInfo download = await blobClient.DownloadAsync(cancellationToken);

            return download.Content;
        }

        public async Task UploadAsync(Stream stream, string contentType, string fileId, string container,
            CancellationToken cancellationToken = default)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(container);
            var blobClient = containerClient.GetBlobClient(fileId);

            await blobClient.UploadAsync(
                stream,
                new BlobHttpHeaders { ContentType = contentType },
                cancellationToken: cancellationToken);
        }
    }
}