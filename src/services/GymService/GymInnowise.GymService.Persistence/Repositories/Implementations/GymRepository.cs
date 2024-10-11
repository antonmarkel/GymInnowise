using GymInnowise.GymService.Persistence.Data;
using GymInnowise.GymService.Persistence.Models.Dtos;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Gym.Enums;
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

        public async Task<IEnumerable<GymPreviewModel>> GetGymsByTagsAsync(IEnumerable<GymTag> tags)
        {
            if (!tags.Any())
            {
                return await GetAllGymsAsync();
            }

            var gyms = await _dbContext.Gyms
                .Select(g => new GymPreviewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Address = g.Address,
                    ContactInfo = g.ContactInfo,
                    Tags = g.Tags
                })
                .AsNoTracking()
                .ToListAsync();

            var filteredGyms = gyms
                .Where(g => g.Tags.Intersect(tags).Any())
                .ToList();

            return filteredGyms;
        }

        public async Task<IEnumerable<GymPreviewModel>> GetAllGymsAsync()
        {
            return await _dbContext.Gyms
                .Select(g => new GymPreviewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Address = g.Address,
                    ContactInfo = g.ContactInfo,
                    Tags = g.Tags
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}