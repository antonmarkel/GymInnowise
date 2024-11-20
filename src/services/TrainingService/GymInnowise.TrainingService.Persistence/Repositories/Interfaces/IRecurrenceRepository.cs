using GymInnowise.TrainingService.Persistence.Entities;

namespace GymInnowise.TrainingService.Persistence.Repositories.Interfaces;

public interface IRecurrenceRepository
{
    Task AddRecurrenceAsync(RecurrenceEntity entity,
        CancellationToken cancellationToken = default);

    Task RemoveRecurrenceAsync(RecurrenceEntity entity, CancellationToken cancellationToken = default);
    Task UpdateRecurrenceAsync(RecurrenceEntity entity, CancellationToken cancellationToken = default);
}