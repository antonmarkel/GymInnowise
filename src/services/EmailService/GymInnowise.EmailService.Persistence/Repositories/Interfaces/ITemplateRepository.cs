using GymInnowise.EmailService.Persistence.Models;

namespace GymInnowise.EmailService.Persistence.Repositories.Interfaces
{
    public interface ITemplateRepository
    {
        Task<TemplateEntity?> GetTemplateAsync(string name);
        Task UpdateTemplateAsync(TemplateEntity entity);
        Task AddTemplateAsync(TemplateEntity entity);
        Task<IEnumerable<string>> GetTemplatesNamesAsync();
    }
}