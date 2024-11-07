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
        private readonly IValidator<Stream> _streamValidator;

        protected FileController(IFileService<TMetadata> fileService, IValidator<Stream> streamValidator)
        {
            _fileService = fileService;
            _streamValidator = streamValidator;
        }

        [HttpGet("{fileId}"), ActionName("GetFile")]
        public async Task<IActionResult> GetFileByIdAsync([FromRoute] Guid fileId, CancellationToken cancellationToken)
        {
            var result = await _fileService.DownloadAsync(fileId, cancellationToken);

            return result.Match<IActionResult>(
                res => File(res.Content, res.Metadata.ContentType, res.Metadata.FileName),
                _ => NotFound("Metadata wasn't found!"),
                _ => NotFound("File wasn't found"));
        }

        [HttpGet("{fileId}/meta")]
        public async Task<IActionResult> GetFileMetadataByIdAsync([FromRoute] Guid fileId)
        {
            var result = await _fileService.GetMetadataByIdAsync(fileId);

            return result.Match<IActionResult>(
                Ok,
                _ => NotFound());
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileAsync([FromBody] TMetadata metadata,
            CancellationToken cancellationToken)
        {
            if (!Request.Body.CanRead ||
                !(await _streamValidator.ValidateAsync(Request.Body, cancellationToken)).IsValid)
            {
                return BadRequest();
            }

            var fileId = await _fileService.UploadAsync(Request.Body, metadata);

            return CreatedAtAction("GetFile", new { fileId }, fileId);
        }
    }
}