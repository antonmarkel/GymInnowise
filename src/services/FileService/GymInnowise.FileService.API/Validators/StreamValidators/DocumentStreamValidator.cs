using FluentValidation;
using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.Shared.Files.Dtos.Base;
using Microsoft.Extensions.Options;

namespace GymInnowise.FileService.API.Validators.FileValidators
{
    public class DocumentStreamValidator : AbstractValidator<Stream>
    {
        private readonly FileSettings _fileSettings;

        public DocumentStreamValidator(IOptions<FileSettings> fileSettings)
        {
            _fileSettings = fileSettings.Value;

            RuleFor(x => x)
                .NotNull().WithMessage("File must be provided.")
                .Must(stream => stream != null && stream.Length <= _fileSettings.MaxDocumentSize)
                .WithMessage($"File size must be less than or equal to {_fileSettings.MaxDocumentSize / 1024} KB");
        }
    }
}