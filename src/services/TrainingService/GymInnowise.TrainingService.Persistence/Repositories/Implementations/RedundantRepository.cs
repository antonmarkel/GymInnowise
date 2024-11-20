using GymInnowise.TrainingService.Persistence.Data;
using GymInnowise.TrainingService.Persistence.Entities.Interfaces;
using GymInnowise.TrainingService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.TrainingService.Persistence.Repositories.Implementations
{
    public class RedundantRepository<TRedundantEntity> : IRedundantRepository<TRedundantEntity>
        where TRedundantEntity : class, IRedundantEntity
    {
        private readonly TrainingServiceDbContext _context;

        public RedundantRepository(TrainingServiceDbContext context)
        {
            _context = context;
        }

        public async Task AddRedundantAsync(TRedundantEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<TRedundantEntity>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRedundantAsync(TRedundantEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TRedundantEntity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveRedundantAsync(TRedundantEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TRedundantEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TRedundantEntity?> GetRedundantAsync(Guid redundantID,
            CancellationToken cancellationToken = default)
        {
            var nullableEntity = await _context.Set<TRedundantEntity>()
                .SingleOrDefaultAsync(red => red.OriginalId == redundantID, cancellationToken);

            return nullableEntity;
        }
    }
}