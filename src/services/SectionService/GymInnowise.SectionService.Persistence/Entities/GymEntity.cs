using GymInnowise.SectionService.Persistence.Entities.Base;

namespace GymInnowise.SectionService.Persistence.Entities
{
    public class GymEntity : IEntity
    {
        public Guid GymId { get; set; }
        public required string Name { get; set; }
        public Guid? ThumbnailId { get; set; }

        public ICollection<SectionEntity> SectionsOnBoard { get; set; } = [];
    }
}
