using GymInnowise.GymService.Persistence.Data;
using GymInnowise.GymService.Persistence.Models.Dtos;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using GymInnowise.GymService.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.GymService.Persistence.Repositories.Implementations
{
    public class GymRepository(GymServiceDbContext _dbContext) : IGymRepository
    {
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

        public async Task<List<GymPreviewDto>> GetGymsByTagsAsync(List<GymTag> tags)
        {
            var gyms = await _dbContext.Gyms
                .Select(g => new
                {
                    g.Id,
                    g.Name,
                    g.Address,
                    g.ContactInfo,
                    g.Tags
                })
                .AsNoTracking()
                .ToListAsync();

            var filteredGyms = gyms
                .Where(g => g.Tags.Intersect(tags).Any())
                .Select(g => new GymPreviewDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Address = g.Address,
                    ContactInfo = g.ContactInfo,
                    Tags = g.Tags
                })
                .ToList();

            return filteredGyms;
        }
    }
}
