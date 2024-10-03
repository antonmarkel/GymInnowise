using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.FileService.Logic.Results;
using GymInnowise.FileService.Logic.Results.Failures;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using GymInnowise.FileService.Persistence.Services.Interfaces;
using GymInnowise.Shared.Files.Dtos.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneOf;

namespace GymInnowise.FileService.Logic.Services
{
    public class DocumentService : IFileService<DocumentMetadata>
    {
        private readonly IFileMetadataRepository<DocumentMetadataEntity> _repo;
        private readonly ILogger<DocumentService> _logger;
        private readonly IBlobService _blobService;
        private readonly string _container;

        public DocumentService(IFileMetadataRepository<DocumentMetadataEntity> repo,
            IBlobService blobService, IOptions<ContainerSettings> containerSettings,
            ILogger<DocumentService> logger)
        {
            _repo = repo;
            _logger = logger;
            _blobService = blobService;
            _container = containerSettings.Value.DocumentContainer;
        }

        public async Task<Guid> UploadAsync(Stream stream, DocumentMetadata metadata,
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
            await _blobService.UploadAsync(stream, metadata.ContentType, metadataEntity.Id.ToString(),
                _container, cancellationToken);
            _logger.LogInformation("Document was uploaded. Info: {@Id}", metadataEntity.Id);

            return metadataEntity.Id;
        }

        public async Task<OneOf<FileResult<DocumentMetadata>, MetadataNotFound, FileNotFound>> DownloadAsync(
            Guid fileId, CancellationToken cancellationToken = default)
        {
            var metadataEntity = await _repo.GetFileMetadataByIdAsync(fileId);
            if (metadataEntity is null)
            {
                _logger.LogWarning("Document's metadata was not found. Info: {@fileId}", fileId);

                return new MetadataNotFound();
            }

            var stream = await _blobService.DownloadAsync(fileId.ToString(), _container,
                cancellationToken);
            if (stream is null)
            {
                _logger.LogWarning("Document's file was not found. Info: {@fileId}", fileId);

                return new FileNotFound();
            }

            _logger.LogInformation("Document was downloaded. Info: {@Id}", metadataEntity.Id);

            return new FileResult<DocumentMetadata> { Content = stream, Metadata = metadataEntity.ToDto() };
        }

        public async Task<OneOf<DocumentMetadata, MetadataNotFound>> GetMetadataByIdAsync(Guid fileId)
        {
            var metadataEntity = await _repo.GetFileMetadataByIdAsync(fileId);
            if (metadataEntity is null)
            {
                _logger.LogWarning("Document's metadata was not found. Info: {@fileId}", fileId);

                return new MetadataNotFound();
            }

            return metadataEntity.ToDto();
        }
    }
}