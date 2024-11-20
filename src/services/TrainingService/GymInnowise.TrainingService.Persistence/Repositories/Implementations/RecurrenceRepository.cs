using GymInnowise.TrainingService.Persistence.Data;
using GymInnowise.TrainingService.Persistence.Entities;
using GymInnowise.TrainingService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.TrainingService.Persistence.Repositories.Implementations
{
    public class RecurrenceRepository : IRecurrenceRepository
    {
        private readonly TrainingServiceDbContext _context;

        public RecurrenceRepository(TrainingServiceDbContext context)
        {
            _context = context;
        }

        public async Task AddRecurrenceAsync(RecurrenceEntity entity,
            CancellationToken cancellationToken = default)
        {
            await _context.Recurrences.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRecurrenceAsync(RecurrenceEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Recurrences.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRecurrenceAsync(RecurrenceEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.Recurrences
                .Where(rec => rec.TrainingId == entity.TrainingId)
                .ExecuteUpdateAsync(setter =>
                        setter.SetProperty(ent => ent.DaysOfWeek, entity.DaysOfWeek)
                            .SetProperty(ent => ent.RecurrenceType, entity.RecurrenceType),
                    cancellationToken);
        }
    }
}