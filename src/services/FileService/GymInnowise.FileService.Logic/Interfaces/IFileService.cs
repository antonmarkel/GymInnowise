using GymInnowise.FileService.Logic.Results;
using GymInnowise.FileService.Logic.Results.Failures;
using GymInnowise.Shared.Files.Dtos.Base;
using OneOf;

namespace GymInnowise.FileService.Logic.Interfaces
{
    public interface IFileService<TMeta> where TMeta : MetadataBase
    {
        Task<Guid> UploadAsync(Stream stream, TMeta metadata);

        Task<OneOf<FileResult<TMeta>, MetadataNotFound, FileNotFound>> DownloadAsync(Guid fileId,
            CancellationToken cancellationToken = default);

        Task<OneOf<TMeta, MetadataNotFound>> GetMetadataByIdAsync(Guid fileId);
    }
}