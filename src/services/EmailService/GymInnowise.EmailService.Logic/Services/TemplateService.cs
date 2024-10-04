using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Persistence.Models;
using GymInnowise.EmailService.Persistence.Repositories.Interfaces;
using GymInnowise.EmailService.Shared.Dtos.Requests;
using GymInnowise.EmailService.Shared.Dtos.Responses;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Services
{
    public class TemplateService(ITemplateRepository _repo) : ITemplateService
    {
        public async Task<OneOf<GetTemplateResponse, NotFound>> GetTemplateByNameAsync(string name)
        {
            var template = await _repo.GetTemplateAsync(name);
            if (template is null)
            {
                return new NotFound();
            }

            var response = new GetTemplateResponse()
            {
                Body = template.Body,
                Subject = template.Subject,
                Data = new Dictionary<string, string>(template.Data),
                TemplateName = template.Name
            };

            return response;
        }

        public async Task<IEnumerable<string>> GetTemplatesNamesAsync()
        {
            return await _repo.GetTemplatesNamesAsync();
        }

        public async Task AddTemplateAsync(CreateTemplateRequest request)
        {
            var entity = new TemplateEntity()
            {
                Body = request.Body,
                Subject = request.Subject,
                Data = request.Data,
                Name = request.TemplateName
            };

            await _repo.AddTemplateAsync(entity);
        }

        public async Task<OneOf<Success, NotFound>> UpdateTemplateAsync(string templateName,
            UpdateTemplateRequest request)
        {
            var entity = await _repo.GetTemplateAsync(templateName);
            if (entity is null)
            {
                return new NotFound();
            }

            entity.Data = request.Data;
            entity.Subject = request.Subject;
            entity.Body = request.Body;
            await _repo.UpdateTemplateAsync(entity);

            return new Success();
        }
    }
}