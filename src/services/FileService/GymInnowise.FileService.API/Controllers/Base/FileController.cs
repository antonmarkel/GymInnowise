using GymInnowise.FileService.API.Models.Base;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.Shared.Files.Dtos.Base;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.FileService.API.Controllers.Base
{
    public abstract class FileController<TMetadata, TFileRequest> : ControllerBase
        where TMetadata : MetadataBase, new()
        where TFileRequest : FileRequestBase
    {
        private readonly IFileService<TMetadata> _fileService;

        protected FileController(IFileService<TMetadata> fileService)
        {
            _fileService = fileService;
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
        public async Task<IActionResult> UploadFileAsync(TFileRequest request, CancellationToken cancellationToken)
        {
            var file = request.File;
            var metadata = new TMetadata
            {
                ContentType = file.ContentType,
                FileName = file.FileName,
                Format = Path.GetExtension(file.FileName),
                FileSize = file.Length,
                CreateAt = DateTime.UtcNow,
                UploadedBy = Guid.NewGuid() // TODO: get from cookies
            };
            var fileId = await _fileService.UploadAsync(file.OpenReadStream(), metadata, cancellationToken);

            return Created(file.FileName, fileId);
        }
    }
}