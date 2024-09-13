using GymInnowise.GymService.Persistence.Models.Entities;

namespace GymInnowise.GymService.Persistence.Repositories.Interfaces
{
    public interface IBlockingEventRepository
    {
        Task AddEventAsync(BlockingEventEntity blockingEventEntity);
        Task UpdateEventAsync(BlockingEventEntity blockingEventEntity);
        Task RemoveEventAsync(Guid id);
        Task<List<BlockingEventEntity>> GetBlockingEventsByGymIdAsync(Guid gymId);
    }
}
