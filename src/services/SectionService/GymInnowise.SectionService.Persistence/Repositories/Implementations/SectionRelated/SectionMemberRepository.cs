using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations.SectionRelated
{
    public class SectionMemberRepository : ISectionRelatedRepository<ProfileEntity>
    {
        private readonly SectionDbContext _context;

        public SectionMemberRepository(SectionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SectionEntity sectionEntity, ProfileEntity memberEntity)
        {
            sectionEntity.Members.Add(memberEntity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(SectionEntity sectionEntity, ProfileEntity memberEntity)
        {
            sectionEntity.Members.Remove(memberEntity);
            await _context.SaveChangesAsync();
        }
    }
}
