using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.FileService.Logic.Results;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using GymInnowise.FileService.Persistence.Services.Interfaces;
using GymInnowise.Shared.Blob.Dtos.Base;
using OneOf;

namespace GymInnowise.FileService.Logic.Services
{
    public class DocumentService : IFileService<DocumentMetadata>
    {
        private readonly IFileMetadataRepository<DocumentMetadataEntity> _repo;
        private readonly IBlobService _blobService;
        private readonly string _container;

        public DocumentService(IFileMetadataRepository<DocumentMetadataEntity> repo,
            IBlobService blobService, string container)
        {
            _repo = repo;
            _blobService = blobService;
            _container = container;
        }

        public async Task UploadAsync(Stream stream, DocumentMetadata metadata,
            CancellationToken cancellationToken = default)
        {
            var metadataEntity = new DocumentMetadataEntity
            {
                ContentType = metadata.ContentType,
                CreateAt = metadata.CreateAt,
                FileName = metadata.FileName,
                FileSize = metadata.FileSize,
                Format = metadata.Format,
                UploadedBy = metadata.UploadedBy
            };

            await _repo.CreateFileMetadataAsync(metadataEntity);
            await _blobService.UploadAsync(stream, metadata.ContentType, metadata.Id.ToString(),
                _container, cancellationToken);
        }

        public async Task<OneOf<FileResult<DocumentMetadata>, MetadataNotFound, FileNotFound>> DownloadAsync(
            Guid fileId, CancellationToken cancellationToken = default)
        {
            var metadataEntity = await _repo.GetFileMetadataByIdAsync(fileId);
            if (metadataEntity is null)
            {
                return new MetadataNotFound();
            }

            var stream = await _blobService.DownloadAsync(fileId.ToString(), _container,
                cancellationToken);
            if (stream is null)
            {
                return new FileNotFound();
            }

            var metadata = new DocumentMetadata()
            {
                ContentType = metadataEntity.ContentType,
                CreateAt = metadataEntity.CreateAt,
                FileName = metadataEntity.FileName,
                FileSize = metadataEntity.FileSize,
                Format = metadataEntity.Format,
                Id = metadataEntity.Id,
                UploadedBy = metadataEntity.UploadedBy
            };

            return new FileResult<DocumentMetadata> { Content = stream, Metadata = metadata };
        }
    }
}