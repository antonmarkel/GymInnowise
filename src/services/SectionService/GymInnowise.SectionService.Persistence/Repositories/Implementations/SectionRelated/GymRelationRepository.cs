using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.SectionService.Persistence.Repositories.Implementations.Abstract;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations.SectionRelated
{
    public class GymRelationRepository : SectionRelationRepository<SectionGymEntity>
    {
        public GymRelationRepository(SectionDbContext context) : base(context)
        {
        }
    }
}
