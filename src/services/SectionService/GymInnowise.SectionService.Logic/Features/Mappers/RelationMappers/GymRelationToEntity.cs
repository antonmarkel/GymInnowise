using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.SectionRelations;

namespace GymInnowise.SectionService.Logic.Features.Mappers.RelationMappers
{
    public class GymRelationToEntity : IMapper<GymRelation, SectionGymEntity>
    {
        public SectionGymEntity Map(GymRelation source)
        {
            return new SectionGymEntity
            {
                RelatedId = source.RelatedId,
                SectionId = source.SectionId,
                AddedOnUtc = source.AddedOnUtc,
                Notes = source.Notes,
            };
        }
    }
}
