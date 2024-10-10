using GymInnowise.Shared.Files.Dtos.Base;

namespace GymInnowise.Shared.Files.Dtos.Metadata
{
    public class ImageMetadata : MetadataBase
    {
        public Guid? ThumbnailId { get; set; }
    }
}