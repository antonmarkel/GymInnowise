using GymInnowise.EmailService.Shared.Dtos.Requests;
using GymInnowise.EmailService.Shared.Dtos.Responses;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Interfaces
{
    public interface ITemplateService
    {
        Task<OneOf<GetTemplateResponse, NotFound>> GetTemplateByNameAsync(string name);
        Task<IEnumerable<string>> GetTemplatesNamesAsync();
        Task AddTemplateAsync(CreateTemplateRequest request);
        Task<OneOf<Success, NotFound>> UpdateTemplateAsync(string templateName, UpdateTemplateRequest request);
    }
}
