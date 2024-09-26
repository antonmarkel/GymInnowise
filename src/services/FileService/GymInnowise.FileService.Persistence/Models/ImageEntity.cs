using GymInnowise.FileService.Persistence.Models.Base;

namespace GymInnowise.FileService.Persistence.Models
{
    public class ImageEntity : FileMetadataBase
    {
        public string ThumbnailUrl { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
