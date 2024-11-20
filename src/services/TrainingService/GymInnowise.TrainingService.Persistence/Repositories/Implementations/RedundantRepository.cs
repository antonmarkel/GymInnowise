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
        private readonly DbSet<TRedundantEntity> _redundant;

        public RedundantRepository(TrainingServiceDbContext context)
        {
            _context = context;
            _redundant = context.Set<TRedundantEntity>();
        }

        public async Task AddRedundantAsync(TRedundantEntity entity, CancellationToken cancellationToken = default)
        {
            await _redundant.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRedundantAsync(TRedundantEntity entity, CancellationToken cancellationToken = default)
        {
            _redundant.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveRedundantAsync(TRedundantEntity entity, CancellationToken cancellationToken = default)
        {
            _redundant.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TRedundantEntity?> GetRedundantAsync(Guid redundantID,
            CancellationToken cancellationToken = default)
        {
            var nullableEntity = await _redundant
                .SingleOrDefaultAsync(red => red.OriginalId == redundantID, cancellationToken);

            return nullableEntity;
        }
    }
}