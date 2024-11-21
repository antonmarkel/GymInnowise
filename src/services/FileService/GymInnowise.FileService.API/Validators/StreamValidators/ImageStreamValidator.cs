using FluentValidation;
using GymInnowise.FileService.Configuration.Blob;
using Microsoft.Extensions.Options;

namespace GymInnowise.FileService.API.Validators.FileValidators
{
    public class ImageStreamValidator : AbstractValidator<Stream>
    {
        private readonly FileSettings _fileSettings;

        public ImageStreamValidator(IOptions<FileSettings> fileSettings)
        {
            _fileSettings = fileSettings.Value;

            RuleFor(stream => stream)
                .NotNull().WithMessage("File must be provided.")
                .Must(stream => stream != null && stream.Length <= _fileSettings.MaxImageSize)
                .WithMessage($"File size must be less than or equal to {_fileSettings.MaxImageSize / 1024} KB");
        }
    }
}