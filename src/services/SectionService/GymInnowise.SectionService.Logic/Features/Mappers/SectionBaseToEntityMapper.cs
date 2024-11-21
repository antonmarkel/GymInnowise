using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.Shared.Sections.Base;

namespace GymInnowise.SectionService.Logic.Features.Mappers
{
    public class SectionBaseToEntityMapper : IMapper<SectionBase, SectionEntity>
    {
        public SectionEntity Map(SectionBase source)
        {
            return new SectionEntity
            {
                CostPerTraining = source.CostPerTraining,
                Description = source.Description,
                IsActive = source.IsActive,
                Name = source.Name,
                Tags = source.Tags,
                ThumbnailId = source.ThumbnailId
            };
        }
    }
}
