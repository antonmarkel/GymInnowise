using GymInnowise.FileService.API.Controllers.Base;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.Shared.Files.Dtos.Base;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.FileService.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : FileController<DocumentMetadata>
    {
        public DocumentController(IFileService<DocumentMetadata> fileService) : base(fileService)
        {
        }
    }
}
