using GymInnowise.SectionService.Persistence.Entities.Base;

namespace GymInnowise.SectionService.Persistence.Entities
{
    public class ProfileEntity : IEntity
    {
        public Guid AccountId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Guid? ThumbnailId { get; set; }

        public ICollection<SectionEntity> VisitedSections { get; set; } = [];
        public ICollection<SectionEntity> MentoredSections { get; set; } = [];
    }
}