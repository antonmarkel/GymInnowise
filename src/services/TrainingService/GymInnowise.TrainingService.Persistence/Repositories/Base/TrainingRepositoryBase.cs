using GymInnowise.Shared.Trainings.Enums;
using GymInnowise.TrainingService.Persistence.Data;
using GymInnowise.TrainingService.Persistence.Entities.Base;
using GymInnowise.TrainingService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.TrainingService.Persistence.Repositories.Base
{
    public abstract class TrainingRepositoryBase<TTrainingEntity> : ITrainingRepository<TTrainingEntity>
        where TTrainingEntity : TrainingEntityBase
    {
        private readonly TrainingServiceDbContext _context;
        private readonly DbSet<TTrainingEntity> _trainings;

        protected TrainingRepositoryBase(TrainingServiceDbContext context)
        {
            _context = context;
            _trainings = _context.Set<TTrainingEntity>();
        }

        public async Task CreateTrainingAsync(TTrainingEntity entity,
            CancellationToken cancellationToken = default)
        {
            await _trainings.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateTrainingGeneralAsync(TTrainingEntity entity,
            CancellationToken cancellationToken = default)
        {
            await _trainings.ExecuteUpdateAsync(setter =>
                    setter.SetProperty(ent => ent.Title, entity.Title)
                        .SetProperty(ent => ent.GymId, entity.GymId)
                        .SetProperty(ent => ent.DateStartUtc, entity.DateStartUtc)
                        .SetProperty(ent => ent.DateEndUtc, entity.DateEndUtc),
                cancellationToken);
        }

        public virtual async Task UpdateTrainingStatusAsync(Guid trainingId, TrainingStatusEnum status,
            CancellationToken cancellationToken = default)
        {
            await _trainings.ExecuteUpdateAsync(setter =>
                    setter.SetProperty(ent => ent.Status, status),
                cancellationToken);
        }

        public virtual async Task UpdateTrainingReportAsync(Guid trainingId, Guid reportId,
            CancellationToken cancellationToken = default)
        {
            await _trainings.ExecuteUpdateAsync(setter =>
                    setter.SetProperty(ent => ent.ReportId, reportId),
                cancellationToken);
        }

        public abstract Task<TTrainingEntity?> GetTrainingByIdAsync(Guid trainingId,
            bool includeReferences = false,
            CancellationToken cancellationToken = default);
    }
}