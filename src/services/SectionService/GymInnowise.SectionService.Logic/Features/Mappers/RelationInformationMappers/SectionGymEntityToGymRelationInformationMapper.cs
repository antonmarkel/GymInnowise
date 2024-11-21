using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.SectionRelations.Information;

namespace GymInnowise.SectionService.Logic.Features.Mappers.RelationInformationMappers
{
    public class SectionGymEntityToGymRelationInformationMapper
        : IMapper<SectionGymEntity, GymRelationInformation>
    {
        public GymRelationInformation Map(SectionGymEntity source)
        {
            return new GymRelationInformation()
            {
                FullName = source.Gym!.Name,
                AddedOnUtc = source.AddedOnUtc,
                Notes = source.Notes,
                RelatedId = source.RelatedId,
                ThumbnailId = source.Gym.ThumbnailId
            };
        }
    }
}
