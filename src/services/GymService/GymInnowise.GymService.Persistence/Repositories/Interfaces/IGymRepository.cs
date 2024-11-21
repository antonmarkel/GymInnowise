using GymInnowise.GymService.Persistence.Models.Dtos;
using GymInnowise.GymService.Persistence.Models.Entities;

namespace GymInnowise.GymService.Persistence.Repositories.Interfaces
{
    public interface IGymRepository
    {
        Task AddGymAsync(GymEntity gymEntity);
        Task UpdateGymAsync(GymEntity gymEntity);
        Task<GymEntity?> GetGymByIdAsync(Guid id);
        Task<IEnumerable<GymPreviewModel>> GetGymsByTagsAsync(IEnumerable<string> tags);
    }
}