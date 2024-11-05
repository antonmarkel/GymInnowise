using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.SectionRelations.Information;

namespace GymInnowise.SectionService.Logic.Features.Mappers.RelationInformationMappers
{
    public class SectionMemberEntityToMembershipInformationMapper
        : IMapper<SectionMemberEntity, MembershipInformation>
    {
        public MembershipInformation Map(SectionMemberEntity source)
        {
            return new MembershipInformation
            {
                FullName = $"{source.Member!.FirstName} {source.Member.LastName}",
                ThumbnailId = source.Member.ThumbnailId,
                AddedOnUtc = source.AddedOnUtc,
                Goal = source.Goal,
                RelatedId = source.RelatedId
            };
        }
    }
}
