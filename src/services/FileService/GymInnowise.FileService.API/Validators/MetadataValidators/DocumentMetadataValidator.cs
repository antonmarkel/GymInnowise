using FluentValidation;
using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.Shared.Files.Dtos.Metadata;
using Microsoft.Extensions.Options;

namespace GymInnowise.FileService.API.Validators.MetadataValidators
{
    public class DocumentMetadataValidator : AbstractValidator<DocumentMetadata>
    {
        public DocumentMetadataValidator(IOptions<FileSettings> settings)
        {
            var _settings = settings.Value;
            RuleFor(metadata => metadata.Format).Must(format => _settings!.DocumentAllowedExtensions.Contains(format))
                .WithMessage("This format is no allowed!");
        }
    }
}
