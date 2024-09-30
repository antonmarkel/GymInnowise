using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.Shared.Blob.Dtos.Base;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.FileService.API.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly IFileService<ImageMetadata> _fileService;

        public ImageController(IFileService<ImageMetadata> fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetImageByIdAsync(Guid fileId, CancellationToken cancellationToken)
        {
            var result = await _fileService.DownloadAsync(fileId, cancellationToken);

            return result.Match<IActionResult>(
                res => File(res.Content, res.Metadata.ContentType, res.Metadata.FileName),
                _ => NotFound("Metadata wasn't found!"),
                _ => NotFound("File wasn't found"));
        }

        [HttpGet("{fileId}/meta")]
        public async Task<IActionResult> GetImageMetadataByIdAsync(Guid fileId)
        {
            var result = await _fileService.GetMetadataByIdAsync(fileId);

            return result.Match<IActionResult>(
                res => Ok(res),
                _ => NotFound());
        }

        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(IFormFile file,
            CancellationToken cancellationToken)
        {
            var metadata = new ImageMetadata()
            {
                ContentType = file.ContentType,
                FileName = file.FileName,
                Format = Path.GetExtension(file.FileName),
                FileSize = file.Length,
                CreateAt = DateTime.UtcNow,
                UploadedBy = Guid.NewGuid() //TODO: get from cookies
            };
            var fileId = await _fileService.UploadAsync(file.OpenReadStream(), metadata, cancellationToken);

            return Created(file.FileName, fileId);
        }
    }
}
