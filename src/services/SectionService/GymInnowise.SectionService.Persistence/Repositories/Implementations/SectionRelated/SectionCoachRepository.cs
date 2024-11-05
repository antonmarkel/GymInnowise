using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations.SectionRelated
{
    public class SectionCoachRepository : ISectionRelatedRepository<ProfileEntity>
    {
        private readonly SectionDbContext _context;

        public SectionCoachRepository(SectionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SectionEntity sectionEntity, ProfileEntity coachEntity)
        {
            sectionEntity.Coaches.Add(coachEntity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(SectionEntity sectionEntity, ProfileEntity coachEntity)
        {
            sectionEntity.Coaches.Remove(coachEntity);
            await _context.SaveChangesAsync();
        }
    }
}
