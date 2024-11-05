using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations.SectionRelated
{
    public class SectionGymRepository : ISectionRelatedRepository<GymEntity>
    {
        private readonly SectionDbContext _context;

        public SectionGymRepository(SectionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SectionEntity sectionEntity, GymEntity gymEntity)
        {
            sectionEntity.Gyms.Add(gymEntity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(SectionEntity sectionEntity, GymEntity gymEntity)
        {
            sectionEntity.Gyms.Remove(gymEntity);
            await _context.SaveChangesAsync();
        }
    }
}
