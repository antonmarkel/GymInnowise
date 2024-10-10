using GymInnowise.FileService.API.Controllers.Base;
using GymInnowise.FileService.API.Validators.FileValidators;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.Shared.Files.Dtos.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.FileService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : FileController<DocumentMetadata>
    {
        public DocumentController(IFileService<DocumentMetadata> fileService, DocumentStreamValidator validator) : base(
            fileService, validator)
        {
        }
    }
}
