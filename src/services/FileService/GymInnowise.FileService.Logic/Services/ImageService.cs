﻿using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.FileService.Logic.Results;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using GymInnowise.FileService.Persistence.Services.Interfaces;
using GymInnowise.Shared.Files.Dtos.Base;
using Microsoft.Extensions.Options;
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
            IThumbnailService thumbnailService, IOptions<ContainerSettings> containerSettings)
        {
            _repo = repo;
            _blobService = blobService;
            _container = containerSettings.Value.ImageContainer;
            _thumbnailService = thumbnailService;
        }

        public async Task<Guid> UploadAsync(Stream stream, ImageMetadata metadata,
            CancellationToken cancellationToken = default)
        {
            var metadataEntity = new ImageMetadataEntity()
            {
                ContentType = metadata.ContentType,
                CreateAt = metadata.CreateAt,
                FileName = metadata.FileName,
                FileSize = metadata.FileSize,
                Format = metadata.Format,
                Id = metadata.Id,
                ThumbnailId = metadata.ThumbnailId,
                UploadedBy = metadata.UploadedBy
            };

            var thumbnailResult = await _thumbnailService.GenerateThumbnailAsync(stream, metadata, cancellationToken);
            if (thumbnailResult.IsT0)
            {
                var thumbnail = thumbnailResult.AsT0;
                metadataEntity.ThumbnailId =
                    await UploadAsync(thumbnail.Content, thumbnail.Metadata, cancellationToken);
            }

            await _repo.CreateFileMetadataAsync(metadataEntity);
            await _blobService.UploadAsync(stream, metadata.ContentType,
                metadataEntity.Id.ToString(),
                _container, cancellationToken);

            return metadataEntity.Id;
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

            return new FileResult<ImageMetadata> { Content = stream, Metadata = metadataEntity.ToDto() };
        }

        public async Task<OneOf<ImageMetadata, MetadataNotFound>> GetMetadataByIdAsync(Guid fileId)
        {
            var metadataEntity = await _repo.GetFileMetadataByIdAsync(fileId);
            if (metadataEntity is null)
            {
                return new MetadataNotFound();
            }

            return metadataEntity.ToDto();
        }
    }
}