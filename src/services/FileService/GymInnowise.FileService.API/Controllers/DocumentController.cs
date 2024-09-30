using GymInnowise.FileService.API.Controllers.Base;
using GymInnowise.FileService.API.Models.Requests;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.Shared.Files.Dtos.Base;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.FileService.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : FileController<DocumentMetadata, DocumentRequest>
    {
        public DocumentController(IFileService<DocumentMetadata> fileService) : base(fileService)
        {
        }
    }
}
