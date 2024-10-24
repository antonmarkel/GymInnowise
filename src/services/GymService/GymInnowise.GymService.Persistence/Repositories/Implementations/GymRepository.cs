using GymInnowise.GymService.Persistence.Data;
using GymInnowise.GymService.Persistence.Models.Dtos;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.GymService.Persistence.Repositories.Implementations
{
    public class GymRepository : IGymRepository
    {
        private readonly GymServiceDbContext _dbContext;

        public GymRepository(GymServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddGymAsync(GymEntity gymEntity)
        {
            await _dbContext.Gyms.AddAsync(gymEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateGymAsync(GymEntity gymEntity)
        {
            _dbContext.Update(gymEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GymEntity?> GetGymByIdAsync(Guid id)
        {
            return await _dbContext.Gyms.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<GymPreviewModel>> GetGymsByTagsAsync(IEnumerable<string> tags)
        {
            var query = _dbContext.Gyms.AsNoTracking();
            if (tags.Any())
            {
                query = query.Where(gym => tags.Any(tag => gym.Tags.Contains(tag)));
            }

            var gyms = await query.Select(gym => new GymPreviewModel()
            {
                Address = gym.Address,
                ContactInfo = gym.ContactInfo,
                Id = gym.Id,
                Name = gym.Name,
                Tags = gym.Tags
            }).ToListAsync();

            return gyms;
        }
    }
}