using GymInnowise.SectionService.Persistence.Entities.Base;

namespace GymInnowise.SectionService.Persistence.Repositories.Interfaces
{
    public interface IRedundantRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task UploadAsync(TEntity entity, CancellationToken cancellationToken);
        Task UpdateByIdAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
