using GymInnowise.GymService.Persistence.Models.Dtos;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Shared.Enums;

namespace GymInnowise.GymService.Persistence.Repositories.Interfaces
{
    public interface IGymRepository
    {
        Task AddGymAsync(GymEntity gymEntity);
        Task UpdateGymAsync(GymEntity gymEntity);
        Task<GymEntity?> GetGymByIdAsync(Guid id);
        Task<List<GymPreviewDto>> GetAllGymsAsync();
        Task<List<GymPreviewDto>> GetGymsByTagsAsync(List<GymTag> tags);
    }
}
