using GymInnowise.FileService.Persistence.Models.Base;

namespace GymInnowise.FileService.Persistence.Models
{
    public class ImageMetadataEntity : MetadataEntityBase
    {
        public Guid? ThumbnailId { get; set; }
    }
}