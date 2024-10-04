using GymInnowise.EmailService.Persistence.Data;
using GymInnowise.EmailService.Persistence.Models;
using GymInnowise.EmailService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.EmailService.Persistence.Repositories.Implementations
{
    public class TemplateRepository(EmailServiceContext _context) : ITemplateRepository
    {
        public async Task<TemplateEntity?> GetTemplateAsync(string name)
        {
            var template = await _context.Templates.SingleOrDefaultAsync(t => t.Name == name);

            return template;
        }

        public async Task UpdateTemplateAsync(TemplateEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddTemplateAsync(TemplateEntity entity)
        {
            await _context.Templates.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetTemplatesNamesAsync()
        {
            return await _context.Templates.Select(t => t.Name).AsNoTracking().ToListAsync();
        }
    }
}
