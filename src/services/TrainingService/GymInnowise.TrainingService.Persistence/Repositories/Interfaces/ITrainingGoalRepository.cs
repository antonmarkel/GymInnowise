using GymInnowise.TrainingService.Persistence.Entities;

namespace GymInnowise.TrainingService.Persistence.Repositories.Interfaces;

public interface ITrainingGoalRepository
{
    Task AddGoalAsync(TrainingGoalEntity entity, CancellationToken cancellationToken = default);
    Task RemoveGoalAsync(TrainingGoalEntity entity, CancellationToken cancellationToken = default);
    Task UpdateGoalAsync(TrainingGoalEntity entity, CancellationToken cancellationToken = default);
}