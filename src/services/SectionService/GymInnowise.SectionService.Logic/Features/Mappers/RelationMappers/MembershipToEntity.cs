using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.SectionRelations;

namespace GymInnowise.SectionService.Logic.Features.Mappers.RelationMappers
{
    internal class MembershipToEntity : IMapper<Membership, SectionMemberEntity>
    {
        public SectionMemberEntity Map(Membership source)
        {
            return new SectionMemberEntity
            {
                RelatedId = source.RelatedId,
                SectionId = source.SectionId,
                AddedOnUtc = source.AddedOnUtc,
                Goal = source.Goal,
            };
        }
    }
}
