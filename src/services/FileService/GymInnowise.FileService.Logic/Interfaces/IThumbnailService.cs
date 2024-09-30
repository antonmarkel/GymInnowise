using GymInnowise.FileService.Logic.Results;
using GymInnowise.Shared.Blob.Dtos.Base;
using OneOf;

namespace GymInnowise.FileService.Logic.Interfaces
{
    public interface IThumbnailService
    {
        Task<OneOf<FileResult<ImageMetadata>, NotNecessary>> GenerateThumbnailAsync(Stream stream,
            ImageMetadata metadata, CancellationToken cancellationToken = default);
    }
}
