using GymInnowise.FileService.Logic.Results;
using GymInnowise.Shared.Blob.Dtos.Base;
using OneOf;

namespace GymInnowise.FileService.Logic.Interfaces
{
    public interface IFileService<T> where T : MetadataBase
    {
        Task UploadAsync(Stream stream, T metadata, CancellationToken cancellationToken = default);

        Task<OneOf<FileResult<T>, MetadataNotFound, FileNotFound>> DownloadAsync(Guid fileId,
            CancellationToken cancellationToken = default);
    }
}
