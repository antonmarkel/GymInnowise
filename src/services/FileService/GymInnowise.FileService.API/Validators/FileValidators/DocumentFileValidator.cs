using FluentValidation;
using GymInnowise.FileService.API.Models.Requests;
using GymInnowise.FileService.Configuration.Blob;
using Microsoft.Extensions.Options;

namespace GymInnowise.FileService.API.Validators.FileValidators
{
    public class DocumentFileValidator : AbstractValidator<DocumentRequest>
    {
        private readonly FileSettings _fileSettings;

        public DocumentFileValidator(IOptions<FileSettings> fileSettings)
        {
            _fileSettings = fileSettings.Value;

            RuleFor(x => x.File)
                .NotNull().WithMessage("File must be provided.")
                .Must(file =>
                    _fileSettings.DocumentAllowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                .WithMessage(
                    $"File extension is not valid. Allowed extensions are: {string.Join(", ", _fileSettings.DocumentAllowedExtensions)}")
                .Must(file => file.Length <= _fileSettings.MaxDocumentSize)
                .WithMessage($"File size must be less than or equal to {_fileSettings.MaxDocumentSize / 1024} KB");
        }
    }
}
