using GymInnowise.Shared.Trainings.Enums;
using GymInnowise.TrainingService.Persistence.Entities.Base;

namespace GymInnowise.TrainingService.Persistence.Repositories.Interfaces;

public interface ITrainingRepository<TTrainingEntity> where TTrainingEntity : TrainingEntityBase
{
    Task CreateTrainingAsync(TTrainingEntity entity,
        CancellationToken cancellationToken = default);

    Task UpdateTrainingGeneralAsync(TTrainingEntity entity,
        CancellationToken cancellationToken = default);

    Task UpdateTrainingStatusAsync(Guid trainingId, TrainingStatusEnum status,
        CancellationToken cancellationToken = default);

    Task UpdateTrainingReportAsync(Guid trainingId, Guid reportId,
        CancellationToken cancellationToken = default);

    Task<TTrainingEntity?> GetTrainingByIdAsync(Guid trainingId, bool includeReferences = false,
        CancellationToken cancellationToken = default);
}