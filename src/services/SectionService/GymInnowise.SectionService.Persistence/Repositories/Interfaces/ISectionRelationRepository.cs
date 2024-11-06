using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.Shared.Sections.Interfaces;

namespace GymInnowise.SectionService.Persistence.Repositories.Interfaces
{
    public interface ISectionRelationRepository<TSectionRelated>
        where TSectionRelated : class, ISectionRelation, IJoinEntity
    {
        Task AddAsync(TSectionRelated relation, CancellationToken cancellationToken = default);
        Task RemoveAsync(TSectionRelated relation, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(TSectionRelated relation, CancellationToken cancellationToken = default);
        Task UpdateAsync(TSectionRelated relation, CancellationToken cancellationToken = default);

        Task<TSectionRelated?> GetAsync(Guid sectionId, Guid relatedId,
            CancellationToken cancellationToken = default);
    }
}
