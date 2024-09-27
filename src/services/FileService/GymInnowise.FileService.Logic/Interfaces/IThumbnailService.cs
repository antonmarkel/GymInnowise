using GymInnowise.FileService.Logic.Results;
using GymInnowise.Shared.Blob.Dtos.Base;

namespace GymInnowise.FileService.Logic.Interfaces
{
    public interface IThumbnailService
    {
        Task<FileResult<ImageMetadata>?> GenerateThumbnailAsync(Stream stream,
            ImageMetadata metadata,
            CancellationToken cancellationToken = default);
    }
}
