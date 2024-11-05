using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.SectionRelations;

namespace GymInnowise.SectionService.Logic.Features.Mappers.RelationMappers
{
    public class MentorshipToEntity : IMapper<Mentorship, SectionCoachEntity>
    {
        public SectionCoachEntity Map(Mentorship source)
        {
            return new SectionCoachEntity
            {
                SectionId = source.SectionId,
                RelatedId = source.RelatedId,
                AddedOnUtc = source.AddedOnUtc,
                Notes = source.Notes,
            };
        }
    }
}
