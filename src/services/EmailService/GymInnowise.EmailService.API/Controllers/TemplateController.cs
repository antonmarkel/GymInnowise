using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Shared.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.EmailService.API.Controllers
{
    [ApiController]
    [Route("api/templates")]
    public class TemplateController(ITemplateService _templateService) : ControllerBase
    {
        [HttpGet("{name}")]
        public async Task<IActionResult> GetTemplateByIdAsync(string name)
        {
            var result = await _templateService.GetTemplateByNameAsync(name);

            return result.Match<IActionResult>(
                res => Ok(res),
                _ => NotFound()
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTemplatesAsync()
        {
            var result = await _templateService.GetTemplatesNamesAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTemplateAsync([FromBody] CreateTemplateRequest request)
        {
            await _templateService.AddTemplateAsync(request);

            return Created();
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateTemplateAsync(string name, [FromBody] UpdateTemplateRequest request)
        {
            var result = await _templateService.UpdateTemplateAsync(name, request);

            return result.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound());
        }
    }
}