using GymInnowise.FileService.Persistence.Models.Base;
using GymInnowise.Shared.Files.Dtos.Metadata;

namespace GymInnowise.FileService.Persistence.Models
{
    public class DocumentMetadataEntity : MetadataEntityBase
    {
        public DocumentMetadata ToDto()
        {
            return new DocumentMetadata()
            {
                ContentType = ContentType,
                CreateAt = CreateAt,
                FileName = FileName,
                FileSize = FileSize,
                Format = Format,
                Id = Id,
                UploadedBy = UploadedBy
            };
        }
    }
}