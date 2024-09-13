using GymInnowise.GymService.Persistence.Data;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.GymService.Persistence.Repositories.Implementations
{
    public class GymEventRepository(GymServiceDbContext _dbContext) : IGymEventRepository
    {
        public async Task AddEventAsync(GymEventEntity gymEventEntity)
        {
            await _dbContext.GymEvents.AddAsync(gymEventEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(GymEventEntity gymEventEntity)
        {
            _dbContext.Update(gymEventEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveEventAsync(Guid id)
        {
            var eventEntity = await _dbContext.GymEvents.SingleOrDefaultAsync(bl => bl.Id == id);
            if (eventEntity is null)
            {
                return;
            }

            _dbContext.GymEvents.Remove(eventEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<GymEventEntity>> GetGymEventsByGymIdAsync(Guid gymId)
        {
            var events = await _dbContext.GymEvents.Where(ev => ev.GymId == gymId).AsNoTracking().ToListAsync();

            return events;
        }

        public async Task<GymEventEntity?> GetGymEventByIdAsync(Guid eventId)
        {
            return await _dbContext.GymEvents.SingleOrDefaultAsync(ev => ev.Id == eventId);
        }
    }
}
