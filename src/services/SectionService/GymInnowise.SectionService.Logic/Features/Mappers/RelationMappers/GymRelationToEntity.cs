using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.Base.Relations;

namespace GymInnowise.SectionService.Logic.Features.Mappers.RelationMappers
{
    public class GymRelationToEntity : IMapper<GymRelationBase, SectionGymEntity>
    {
        public SectionGymEntity Map(GymRelationBase source)
        {
            return new SectionGymEntity
            {
                RelatedId = source.RelatedId,
                SectionId = source.SectionId,
                Notes = source.Notes,
            };
        }
    }
}
