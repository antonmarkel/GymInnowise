using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.SectionService.Persistence.Repositories.Implementations.Abstract;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations.SectionRelated
{
    public class MembershipRepository : SectionRelationRepository<SectionMemberEntity>
    {
        public MembershipRepository(SectionDbContext context) : base(context)
        {
        }
    }
}
