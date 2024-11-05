using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations.SectionRelated
{
    public class SectionMemberRepository : ISectionRelatedRepository<SectionMemberEntity>
    {
        private readonly SectionDbContext _context;

        public SectionMemberRepository(SectionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SectionMemberEntity relation, CancellationToken cancellationToken = default)
        {
            await _context.SectionMembers.AddAsync(relation, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(SectionMemberEntity relation, CancellationToken cancellationToken = default)
        {
            _context.SectionMembers.Remove(relation);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
