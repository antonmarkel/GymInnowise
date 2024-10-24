namespace GymInnowise.FileService.Configuration.Blob
{
    public class FileSettings
    {
        public IReadOnlyList<string> ImageAllowedExtensions { get; set; } = [];
        public IReadOnlyList<string> DocumentAllowedExtensions { get; set; } = [];
        public long MaxImageSize { get; set; }
        public long MaxDocumentSize { get; set; }
    }
}
