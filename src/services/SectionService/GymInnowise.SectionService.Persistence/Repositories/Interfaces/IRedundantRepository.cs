using GymInnowise.SectionService.Persistence.Entities.Base;

namespace GymInnowise.SectionService.Persistence.Repositories.Interfaces
{
    public interface IRedundantRepository<in TEntity>
        where TEntity : class, IEntity
    {
        Task UploadAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateByIdAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
