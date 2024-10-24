namespace GymInnowise.FileService.Configuration.Blob
{
    public class ThumbnailSettings
    {
        public long MaxFileSizeWithoutThumbnail { get; set; }
        public uint ThumbnailWidth { get; set; }
        public uint ThumbnailHeight { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
    }
}
