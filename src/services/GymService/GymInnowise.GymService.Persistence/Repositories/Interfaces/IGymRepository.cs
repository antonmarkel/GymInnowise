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
        Task<List<GymPreviewModel>> GetAllGymsAsync();
        Task<List<GymPreviewModel>> GetGymsByTagsAsync(List<GymTag> tags);
    }
}