namespace GymInnowise.FileService.API.Models.Base
{
    public abstract class FileRequestBase
    {
        public required IFormFile File { get; set; }
    }
}
