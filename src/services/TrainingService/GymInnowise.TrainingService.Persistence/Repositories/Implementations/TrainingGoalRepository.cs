using GymInnowise.TrainingService.Persistence.Data;
using GymInnowise.TrainingService.Persistence.Entities;
using GymInnowise.TrainingService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.TrainingService.Persistence.Repositories.Implementations
{
    public class TrainingGoalRepository : ITrainingGoalRepository
    {
        private readonly TrainingServiceDbContext _context;

        public TrainingGoalRepository(TrainingServiceDbContext context)
        {
            _context = context;
        }

        public async Task AddGoalAsync(TrainingGoalEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.TrainingGoals.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveGoalAsync(TrainingGoalEntity entity, CancellationToken cancellationToken = default)
        {
            _context.TrainingGoals.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateGoalAsync(TrainingGoalEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.TrainingGoals
                .Where(goal => goal.GoalId == entity.GoalId)
                .ExecuteUpdateAsync(setter =>
                        setter.SetProperty(ent => ent.Description, entity.Description)
                            .SetProperty(ent => ent.Goal, entity.Goal)
                            .SetProperty(ent => ent.Value, entity.Value)
                            .SetProperty(ent => ent.IsCancelled, entity.IsCancelled),
                    cancellationToken);
        }
    }
}
