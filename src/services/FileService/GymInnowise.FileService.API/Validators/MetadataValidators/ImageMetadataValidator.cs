using FluentValidation;
using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.Shared.Files.Dtos.Metadata;
using Microsoft.Extensions.Options;

namespace GymInnowise.FileService.API.Validators.MetadataValidators
{
    public class ImageMetadataValidator : AbstractValidator<ImageMetadata>
    {
        private readonly FileSettings _settings;

        public ImageMetadataValidator(IOptions<FileSettings> settings)
        {
            _settings = settings.Value;
        }

        public ImageMetadataValidator()
        {
            RuleFor(metadata => metadata.Format).Must(format => _settings!.ImageAllowedExtensions.Contains(format))
                .WithMessage("This format is not allowed");
        }
    }
}
