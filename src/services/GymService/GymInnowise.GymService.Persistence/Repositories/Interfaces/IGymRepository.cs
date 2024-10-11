using GymInnowise.GymService.Persistence.Models.Dtos;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.Shared.Gym.Enums;

namespace GymInnowise.GymService.Persistence.Repositories.Interfaces
{
    public interface IGymRepository
    {
        Task AddGymAsync(GymEntity gymEntity);
        Task UpdateGymAsync(GymEntity gymEntity);
        Task<GymEntity?> GetGymByIdAsync(Guid id);
        Task<IEnumerable<GymPreviewModel>> GetAllGymsAsync();
        Task<IEnumerable<GymPreviewModel>> GetGymsByTagsAsync(IEnumerable<GymTag> tags);
    }
}