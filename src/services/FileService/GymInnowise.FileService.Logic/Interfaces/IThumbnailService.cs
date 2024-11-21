using GymInnowise.FileService.Logic.Results;
using GymInnowise.FileService.Logic.Results.Failures;
using GymInnowise.Shared.Files.Dtos.Metadata;
using OneOf;

namespace GymInnowise.FileService.Logic.Interfaces
{
    public interface IThumbnailService
    {
        Task<OneOf<FileResult<ImageMetadata>, NotNecessary>> GenerateThumbnailAsync(Stream stream,
            ImageMetadata metadata, CancellationToken cancellationToken = default);
    }
}