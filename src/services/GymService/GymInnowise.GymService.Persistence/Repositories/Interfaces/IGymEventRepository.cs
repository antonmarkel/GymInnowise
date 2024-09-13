﻿using GymInnowise.GymService.Persistence.Models.Entities;

namespace GymInnowise.GymService.Persistence.Repositories.Interfaces
{
    public interface IGymEventRepository
    {
        Task AddEventAsync(GymEventEntity gymEventEntity);
        Task UpdateEventAsync(GymEventEntity gymEventEntity);
        Task RemoveEventAsync(Guid id);
        Task<List<GymEventEntity>> GetBlockingEventsByGymIdAsync(Guid gymId);
    }
}
