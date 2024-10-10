using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.FileService.Logic.Results;
using GymInnowise.FileService.Logic.Results.Failures;
using GymInnowise.Shared.Files.Dtos.Metadata;
using ImageMagick;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneOf;

namespace GymInnowise.FileService.Logic.Services
{
    public class ThumbnailService : IThumbnailService
    {
        private readonly ThumbnailSettings _thumbnailSettings;
        private readonly ILogger<ThumbnailService> _logger;

        public ThumbnailService(IOptions<ThumbnailSettings> thumbnailSettings, ILogger<ThumbnailService> logger)
        {
            _thumbnailSettings = thumbnailSettings.Value;
            _logger = logger;
        }

        public async Task<OneOf<FileResult<ImageMetadata>, NotNecessary>> GenerateThumbnailAsync(Stream stream,
            ImageMetadata metadata,
            CancellationToken cancellationToken = default)
        {
            if (metadata.FileSize < _thumbnailSettings.MaxFileSizeWithoutThumbnail)
            {
                _logger.LogInformation("Thumbnail for the image is not necessary");

                return new NotNecessary();
            }

            using var image = new MagickImage(stream);
            image.Resize(new MagickGeometry()
            {
                Width = _thumbnailSettings.ThumbnailWidth,
                Height = _thumbnailSettings.ThumbnailHeight,
            });

            var outputStream = new MemoryStream();
            await image.WriteAsync(outputStream, MagickFormat.Jpeg, cancellationToken);
            _logger.LogInformation("Thumbnail image stream was created");
            outputStream.Position = stream.Position = 0;
            if (outputStream.Length > _thumbnailSettings.MaxFileSizeWithoutThumbnail)
            {
                throw new InvalidOperationException("Resized image exceeds the size limit!");
            }

            var newMetadata = new ImageMetadata
            {
                ContentType = _thumbnailSettings.ContentType,
                CreateAt = DateTime.UtcNow,
                FileName = $"thumbnail_{metadata.FileName}",
                FileSize = outputStream.Length,
                Format = _thumbnailSettings.Format,
                UploadedBy = metadata.UploadedBy,
                Id = Guid.NewGuid()
            };
            _logger.LogInformation("Thumbnail was generated");

            return new FileResult<ImageMetadata> { Content = outputStream, Metadata = newMetadata };
        }
    }
}