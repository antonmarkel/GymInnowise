using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.Shared.Sections.Redundant;

namespace GymInnowise.SectionService.Logic.Features.Mappers
{
    public class GymToEntityMapper : IMapper<Gym, GymEntity>
    {
        public GymEntity Map(Gym source)
        {
            return new GymEntity
            {
                PrimaryId = source.PrimaryId,
                Name = source.Name,
                ThumbnailId = source.ThumbnailId
            };
        }
    }
}
