using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.FileService.Logic.Results;
using GymInnowise.Shared.Blob.Dtos.Base;
using ImageMagick;
using Microsoft.Extensions.Options;

namespace GymInnowise.FileService.Logic.Services
{
    public class ThumbnailService : IThumbnailService
    {
        private readonly ThumbnailSettings _thumbnailSettings;

        public ThumbnailService(IOptions<ThumbnailSettings> thumbnailSettings)
        {
            _thumbnailSettings = thumbnailSettings.Value;
        }

        public async Task<FileResult<ImageMetadata>?> GenerateThumbnailAsync(Stream stream,
            ImageMetadata metadata,
            CancellationToken cancellationToken = default)
        {
            if (metadata.FileSize < _thumbnailSettings.MaxFileSizeWithoutThumbnail)
            {
                return null;
            }

            using var image = new MagickImage(stream);
            image.Resize(new MagickGeometry()
            {
                Width = _thumbnailSettings.ThumbnailWidth,
                Height = _thumbnailSettings.ThumbnailHeight,
            });

            var outputStream = new MemoryStream();
            await image.WriteAsync(outputStream, MagickFormat.Jpeg, cancellationToken);
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

            return new FileResult<ImageMetadata> { Content = outputStream, Metadata = newMetadata };
        }
    }
}
