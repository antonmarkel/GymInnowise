﻿using GymInnowise.FileService.API.Controllers.Base;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.Shared.Blob.Dtos.Base;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.FileService.API.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : FileController<ImageMetadata>
    {
        public ImageController(IFileService<ImageMetadata> fileService) : base(fileService)
        {
        }
    }
}
