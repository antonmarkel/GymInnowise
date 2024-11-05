using GymInnowise.SectionService.Persistence.Entities.Base;

namespace GymInnowise.SectionService.Persistence.Repositories.Interfaces
{
    public interface ISectionRelatedRepository<TSectionRelated>
        where TSectionRelated : class, IJoinEntity
    {
        Task AddAsync(TSectionRelated relation, CancellationToken cancellationToken = default);
        Task RemoveAsync(TSectionRelated relation, CancellationToken cancellationToken = default);
    }
}
