namespace GymInnowise.FileService.Configuration.Blob
{
    public class ContainerSettings
    {
        public required string ImageContainer { get; set; }
        public required string DocumentContainer { get; set; }
        public required string ThumbnailContainer { get; set; }
    }
}
