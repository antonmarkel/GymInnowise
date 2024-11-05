using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.Shared.Sections.Base;

namespace GymInnowise.SectionService.Persistence.Entities
{
    public class ProfileEntity : ProfileBase, IEntity
    {
        public Guid Id { get; set; }
        public ICollection<SectionEntity> VisitedSections { get; set; } = [];
        public ICollection<SectionEntity> MentoredSections { get; set; } = [];
    }
}