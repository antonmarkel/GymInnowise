using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Implementations.Abstract;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations
{
    public class GymRepository : RedundantRepository<GymEntity>
    {
        public GymRepository(SectionDbContext context) : base(context)
        {
        }
    }
}