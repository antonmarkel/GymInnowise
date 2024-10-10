using FluentValidation;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.Shared.Files.Dtos.Base;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.FileService.API.Controllers.Base
{
    public abstract class FileController<TMetadata> : ControllerBase
        where TMetadata : MetadataBase, new()
    {
        private readonly IFileService<TMetadata> _fileService;
        private readonly AbstractValidator<Stream> _streamValidator;

        protected FileController(IFileService<TMetadata> fileService, AbstractValidator<Stream> streamValidator)
        {
            _fileService = fileService;
            _streamValidator = streamValidator;
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetFileByIdAsync(Guid fileId, CancellationToken cancellationToken)
        {
            var result = await _fileService.DownloadAsync(fileId, cancellationToken);

            return result.Match<IActionResult>(
                res => File(res.Content, res.Metadata.ContentType, res.Metadata.FileName),
                _ => NotFound("Metadata wasn't found!"),
                _ => NotFound("File wasn't found"));
        }

        [HttpGet("{fileId}/meta")]
        public async Task<IActionResult> GetFileMetadataByIdAsync(Guid fileId)
        {
            var result = await _fileService.GetMetadataByIdAsync(fileId);

            return result.Match<IActionResult>(
                res => Ok(res),
                _ => NotFound());
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(TMetadata metadata, CancellationToken cancellationToken)
        {
            if (!Request.Body.CanRead ||
                !(await _streamValidator.ValidateAsync(Request.Body, cancellationToken)).IsValid)
            {
                return BadRequest();
            }

            var fileId = await _fileService.UploadAsync(Request.Body, metadata);

            return Created(fileId.ToString(), fileId);
        }
    }
}