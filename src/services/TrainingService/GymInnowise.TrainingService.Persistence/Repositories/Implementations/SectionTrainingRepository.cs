using GymInnowise.TrainingService.Persistence.Data;
using GymInnowise.TrainingService.Persistence.Entities.Trainings;
using GymInnowise.TrainingService.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.TrainingService.Persistence.Repositories.Implementations
{
    public class SectionTrainingRepository : TrainingRepositoryBase<SectionTrainingEntity>
    {
        private readonly TrainingServiceDbContext _context;

        public SectionTrainingRepository(TrainingServiceDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<SectionTrainingEntity?> GetTrainingByIdAsync(Guid trainingId,
            bool includeReferences = false,
            CancellationToken cancellationToken = default)
        {
            var query = _context.SectionTrainings.AsQueryable();
            if (includeReferences)
            {
                query = IncludeBaseReferencesAsSplitQuery(query);
                query = query.Include(training => training.Section);
            }

            var entity = await query
                .SingleOrDefaultAsync(training => training.Id == trainingId, cancellationToken);

            return entity;
        }
    }
}
