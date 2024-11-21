using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Implementations.Abstract;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations
{
    public class ProfileRepository : RedundantRepository<ProfileEntity>
    {
        public ProfileRepository(SectionDbContext context) : base(context)
        {
        }
    }
}