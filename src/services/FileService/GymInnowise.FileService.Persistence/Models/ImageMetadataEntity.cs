using GymInnowise.FileService.Persistence.Models.Base;
using GymInnowise.Shared.Files.Dtos.Base;

namespace GymInnowise.FileService.Persistence.Models
{
    public class ImageMetadataEntity : MetadataEntityBase
    {
        public Guid? ThumbnailId { get; set; }

        public ImageMetadata ToDto()
        {
            return new ImageMetadata()
            {
                ContentType = ContentType,
                CreateAt = CreateAt,
                FileName = FileName,
                FileSize = FileSize,
                Format = Format,
                Id = Id,
                ThumbnailId = ThumbnailId,
                UploadedBy = UploadedBy
            };
        }
    }
}