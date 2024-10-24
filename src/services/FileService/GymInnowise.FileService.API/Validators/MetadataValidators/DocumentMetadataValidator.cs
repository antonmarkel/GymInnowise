using FluentValidation;
using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.Shared.Files.Dtos.Metadata;
using Microsoft.Extensions.Options;

namespace GymInnowise.FileService.API.Validators.MetadataValidators
{
    public class DocumentMetadataValidator : AbstractValidator<DocumentMetadata>
    {
        private readonly FileSettings _settings;

        public DocumentMetadataValidator(IOptions<FileSettings> settings)
        {
            _settings = settings.Value;
        }

        public DocumentMetadataValidator()
        {
            RuleFor(metadata => metadata.Format).Must(format => _settings.DocumentAllowedExtensions.Contains(format))
                .WithMessage("This format is no allowed!");
        }
    }
}
