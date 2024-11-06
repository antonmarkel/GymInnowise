using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.Base.Relations;

namespace GymInnowise.SectionService.Logic.Features.Mappers.RelationMappers
{
    public class MentorshipToEntity : IMapper<MentorshipBase, SectionCoachEntity>
    {
        public SectionCoachEntity Map(MentorshipBase source)
        {
            return new SectionCoachEntity
            {
                SectionId = source.SectionId,
                RelatedId = source.RelatedId,
                Notes = source.Notes,
            };
        }
    }
}
