using GymInnowise.FileService.API.Controllers.Base;
using GymInnowise.FileService.API.Models.Requests;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.Shared.Files.Dtos.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.FileService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/images")]
    public class ImageController : FileController<ImageMetadata, ImageRequest>
    {
        public ImageController(IFileService<ImageMetadata> fileService) : base(fileService)
        {
        }
    }
}