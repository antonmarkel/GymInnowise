namespace GymInnowise.Shared.Blob.Dtos.Base
{
    public abstract class MetadataBase
    {
        public Guid Id { get; set; }
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public required string Format { get; set; }
        public long FileSize { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid UploadedBy { get; set; }
    }
}
