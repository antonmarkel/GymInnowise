using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations.Abstract
{
    public abstract class SectionRelationRepository<TRelationEntity> : ISectionRelationRepository<TRelationEntity>
        where TRelationEntity : class, ISectionRelation, IJoinEntity

    {
        private readonly SectionDbContext _context;

        public SectionRelationRepository(SectionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TRelationEntity relation, CancellationToken cancellationToken = default)
        {
            await _context.Set<TRelationEntity>().AddAsync(relation, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(TRelationEntity relation, CancellationToken cancellationToken = default)
        {
            _context.Set<TRelationEntity>().Remove(relation);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(TRelationEntity relation, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TRelationEntity>().AnyAsync(
                rel => rel.RelatedId == relation.RelatedId
                       && rel.SectionId == relation.SectionId,
                cancellationToken);
        }

        public async Task UpdateAsync(TRelationEntity relation, CancellationToken cancellationToken = default)
        {
            _context.Set<TRelationEntity>().Update(relation);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}