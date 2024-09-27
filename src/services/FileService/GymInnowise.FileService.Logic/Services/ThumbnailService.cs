using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.FileService.Logic.Results;
using GymInnowise.Shared.Blob.Dtos.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace GymInnowise.FileService.Logic.Services
{
    public class ThumbnailService : IThumbnailService
    {
        private readonly ThumbnailSettings _thumbnailSettings;

        public ThumbnailService(ThumbnailSettings thumbnailSettings)
        {
            _thumbnailSettings = thumbnailSettings;
        }

        public async Task<FileResult<ImageMetadata>?> GenerateThumbnailAsync(Stream stream,
            ImageMetadata metadata,
            CancellationToken cancellationToken = default)
        {
            if (metadata.FileSize < _thumbnailSettings.MaxFileSizeWithoutThumbnail)
            {
                return null;
            }

            using var image = await Image.LoadAsync(stream, cancellationToken);
            image.Mutate(im => im.Resize(_thumbnailSettings.ThumbnailWidth, _thumbnailSettings.ThumbnailHeight));

            var outputStream = new MemoryStream();
            await image.SaveAsJpegAsync(outputStream, cancellationToken);

            if (outputStream.Length > _thumbnailSettings.MaxFileSizeWithoutThumbnail)
            {
                throw new InvalidOperationException("Resized image exceeds the size limit!");
            }

            metadata.FileSize = outputStream.Length;
            metadata.FileName = $"thumbnail_{metadata.FileName}";

            return new FileResult<ImageMetadata> { Content = outputStream, Metadata = metadata };
        }
    }
}
