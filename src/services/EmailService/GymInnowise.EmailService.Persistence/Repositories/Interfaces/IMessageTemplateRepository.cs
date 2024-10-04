using GymInnowise.EmailService.Persistence.Models;

namespace GymInnowise.EmailService.Persistence.Repositories.Interfaces
{
    public interface IMessageTemplateRepository
    {
        Task<MessageTemplateEntity?> GetTemplateAsync(string name);
        Task UpdateTemplateAsync(MessageTemplateEntity entity);
        Task AddTemplateAsync(MessageTemplateEntity entity);
        Task<IEnumerable<string>> GetTemplatesNamesAsync();
    }
}