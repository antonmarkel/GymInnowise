using GymInnowise.TrainingService.Persistence.Data;
using GymInnowise.TrainingService.Persistence.Entities.Trainings;
using GymInnowise.TrainingService.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.TrainingService.Persistence.Repositories.Implementations
{
    public class IndividualWithCoachTrainingRepository
        : TrainingRepositoryBase<IndividualWithCoachTrainingEntity>
    {
        private readonly TrainingServiceDbContext _context;

        public IndividualWithCoachTrainingRepository(TrainingServiceDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IndividualWithCoachTrainingEntity?> GetTrainingByIdAsync(Guid trainingId,
            bool includeReferences = false, CancellationToken cancellationToken = default)
        {
            var query = _context.IndividualWithCoachTrainings.AsQueryable();
            if (includeReferences)
            {
                query = query.Include(training => training.Account)
                    .Include(training => training.Coach);
            }

            var entity = await query
                .SingleOrDefaultAsync(training => training.Id == trainingId, cancellationToken);

            return entity;
        }
    }
}
