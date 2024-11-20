using GymInnowise.TrainingService.Persistence.Entities.Interfaces;

namespace GymInnowise.TrainingService.Persistence.Repositories.Interfaces;

public interface IRedundantRepository<TRedundantEntity> where TRedundantEntity : class, IRedundantEntity
{
    Task AddRedundantAsync(TRedundantEntity entity, CancellationToken cancellationToken = default);
    Task UpdateRedundantAsync(TRedundantEntity entity, CancellationToken cancellationToken = default);
    Task RemoveRedundantAsync(TRedundantEntity entity, CancellationToken cancellationToken = default);

    Task<TRedundantEntity?> GetRedundantAsync(Guid redundantID,
        CancellationToken cancellationToken = default);
}