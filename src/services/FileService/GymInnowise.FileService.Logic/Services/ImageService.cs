using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.FileService.Logic.Results;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using GymInnowise.FileService.Persistence.Services.Interfaces;
using GymInnowise.Shared.Blob.Dtos.Base;
using OneOf;

namespace GymInnowise.FileService.Logic.Services
{
    public class ImageService : IFileService<ImageMetadata>
    {
        private readonly IFileMetadataRepository<ImageMetadataEntity> _repo;
        private readonly IBlobService _blobService;
        private readonly IThumbnailService _thumbnailService;
        private readonly string _container;

        public ImageService(IFileMetadataRepository<ImageMetadataEntity> repo, IBlobService blobService,
            IThumbnailService thumbnailService, ContainerSettings containerSettings)
        {
            _repo = repo;
            _blobService = blobService;
            _container = containerSettings.ImageContainer;
            _thumbnailService = thumbnailService;
        }

        public async Task UploadAsync(Stream stream, ImageMetadata metadata,
            CancellationToken cancellationToken = default)
        {
            var metadataEntity = new ImageMetadataEntity()
            {
                ContentType = metadata.ContentType,
                CreateAt = metadata.CreateAt,
                FileName = metadata.FileName,
                FileSize = metadata.FileSize,
                Format = metadata.Format,
                ThumbnailId = metadata.ThumbnailId,
                UploadedBy = metadata.UploadedBy
            };

            await _repo.CreateFileMetadataAsync(metadataEntity);
            await _blobService.UploadAsync(stream, metadata.ContentType, metadata.Id.ToString(),
                _container, cancellationToken);

            var thumbnail = await _thumbnailService.GenerateThumbnailAsync(stream, metadata, cancellationToken);
            if (thumbnail != null)
            {
                await this.UploadAsync(thumbnail.Content, thumbnail.Metadata, cancellationToken);
            }
        }

        public async Task<OneOf<FileResult<ImageMetadata>, MetadataNotFound, FileNotFound>>
            DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
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

            var metadata = new ImageMetadata
            {
                ContentType = metadataEntity.ContentType,
                CreateAt = metadataEntity.CreateAt,
                FileName = metadataEntity.FileName,
                FileSize = metadataEntity.FileSize,
                Format = metadataEntity.Format,
                Id = metadataEntity.Id,
                ThumbnailId = metadataEntity.ThumbnailId,
                UploadedBy = metadataEntity.UploadedBy
            };

            return new FileResult<ImageMetadata> { Content = stream, Metadata = metadata };
        }
    }
}