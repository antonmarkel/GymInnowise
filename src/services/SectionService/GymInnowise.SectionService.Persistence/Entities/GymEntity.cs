using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.Shared.Sections.Base;

namespace GymInnowise.SectionService.Persistence.Entities
{
    public class GymEntity : GymBase, IEntity
    {
        public Guid Id { get; set; }
        public ICollection<SectionEntity> SectionsOnBoard { get; set; } = [];
    }
}
