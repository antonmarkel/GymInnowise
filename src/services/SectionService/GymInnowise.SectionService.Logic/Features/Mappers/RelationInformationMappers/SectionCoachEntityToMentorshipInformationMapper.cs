using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.SectionRelations.Information;

namespace GymInnowise.SectionService.Logic.Features.Mappers.RelationInformationMappers
{
    public class SectionCoachEntityToMentorshipInformationMapper :
        IMapper<SectionCoachEntity, MentorshipInformation>
    {
        public MentorshipInformation Map(SectionCoachEntity source)
        {
            return new MentorshipInformation
            {
                FullName = $"{source.Coach!.FirstName} {source.Coach.LastName}",
                ThumbnailId = source.Coach.ThumbnailId,
                AddedOnUtc = source.AddedOnUtc,
                Notes = source.Notes,
                RelatedId = source.RelatedId
            };
        }
    }
}
