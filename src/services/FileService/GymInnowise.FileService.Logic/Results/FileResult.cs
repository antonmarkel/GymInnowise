using GymInnowise.Shared.Files.Dtos.Base;

namespace GymInnowise.FileService.Logic.Results
{
    public class FileResult<TMeta> where TMeta : MetadataBase
    {
        public TMeta Metadata { get; set; } = null!;
        public Stream Content { get; set; } = null!;
    }
}