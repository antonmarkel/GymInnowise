using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations.SectionRelated
{
    public class SectionCoachRepository : ISectionRelatedRepository<SectionCoachEntity>
    {
        private readonly SectionDbContext _context;

        public SectionCoachRepository(SectionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SectionCoachEntity relation, CancellationToken cancellationToken = default)
        {
            await _context.SectionCoaches.AddAsync(relation, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(SectionCoachEntity relation, CancellationToken cancellationToken = default)
        {
            _context.SectionCoaches.Remove(relation);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
