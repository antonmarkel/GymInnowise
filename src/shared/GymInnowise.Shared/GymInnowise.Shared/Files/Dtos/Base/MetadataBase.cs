namespace GymInnowise.Shared.Files.Dtos.Base
{
    public abstract class MetadataBase
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid UploadedBy { get; set; }
    }
}