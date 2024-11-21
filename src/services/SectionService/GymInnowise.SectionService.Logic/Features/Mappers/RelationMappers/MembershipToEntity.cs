using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.Base.Relations;

namespace GymInnowise.SectionService.Logic.Features.Mappers.RelationMappers
{
    internal class MembershipToEntity : IMapper<MembershipBase, SectionMemberEntity>
    {
        public SectionMemberEntity Map(MembershipBase source)
        {
            return new SectionMemberEntity
            {
                RelatedId = source.RelatedId,
                SectionId = source.SectionId,
                Goal = source.Goal,
            };
        }
    }
}
