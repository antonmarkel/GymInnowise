using GymInnowise.GymService.Persistence.Data;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.GymService.Persistence.Repositories.Implementations
{
    public class BlockingEventRepository(GymServiceDbContext _dbContext) : IBlockingEventRepository
    {
        public async Task AddEventAsync(BlockingEventEntity blockingEventEntity)
        {
            await _dbContext.BlockingEvents.AddAsync(blockingEventEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(BlockingEventEntity blockingEventEntity)
        {
            _dbContext.Update(blockingEventEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveEventAsync(Guid id)
        {
            var eventEntity = await _dbContext.BlockingEvents.SingleOrDefaultAsync(bl => bl.Id == id);
            if (eventEntity is null)
            {
                return;
            }

            _dbContext.BlockingEvents.Remove(eventEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BlockingEventEntity>> GetBlockingEventsByGymIdAsync(Guid gymId)
        {
            var events = await _dbContext.BlockingEvents.Where(bl => bl.GymId == gymId).AsNoTracking().ToListAsync();

            return events;
        }
    }
}
