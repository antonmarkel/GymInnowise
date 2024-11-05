using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations.SectionRelated
{
    public class SectionGymRepository : ISectionRelatedRepository<SectionGymEntity>
    {
        private readonly SectionDbContext _context;

        public SectionGymRepository(SectionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SectionGymEntity relation, CancellationToken cancellationToken = default)
        {
            await _context.SectionGyms.AddAsync(relation, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(SectionGymEntity relation, CancellationToken cancellationToken = default)
        {
            _context.SectionGyms.Remove(relation);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
