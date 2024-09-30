using FluentValidation;
using GymInnowise.FileService.API.Models.Requests;
using GymInnowise.FileService.Configuration.Blob;
using Microsoft.Extensions.Options;

namespace GymInnowise.FileService.API.Validators.FileValidators
{
    public class ImageFileValidator : AbstractValidator<ImageRequest>
    {
        private readonly FileSettings _fileSettings;

        public ImageFileValidator(IOptions<FileSettings> fileSettings)
        {
            _fileSettings = fileSettings.Value;

            RuleFor(x => x.File)
                .NotNull().WithMessage("File must be provided.")
                .Must(file =>
                    file != null &&
                    _fileSettings.ImageAllowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                .WithMessage(
                    $"File extension is not valid. Allowed extensions are: {string.Join(", ", _fileSettings.ImageAllowedExtensions)}")
                .Must(file => file != null && file.Length <= _fileSettings.MaxImageSize)
                .WithMessage($"File size must be less than or equal to {_fileSettings.MaxImageSize / 1024} KB");
        }
    }
}