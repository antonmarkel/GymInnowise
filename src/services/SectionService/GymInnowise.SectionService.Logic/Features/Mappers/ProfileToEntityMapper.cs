using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.Shared.Sections.Redundant;

namespace GymInnowise.SectionService.Logic.Features.Mappers
{
    public class ProfileToEntityMapper : IMapper<Profile, ProfileEntity>
    {
        public ProfileEntity Map(Profile source)
        {
            return new ProfileEntity
            {
                PrimaryId = source.PrimaryId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                ThumbnailId = source.ThumbnailId,
                Role = source.Role
            };
        }
    }
}
