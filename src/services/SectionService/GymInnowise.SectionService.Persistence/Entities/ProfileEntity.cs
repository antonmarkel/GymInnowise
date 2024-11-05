using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.Base;

namespace GymInnowise.SectionService.Persistence.Entities
{
    public class ProfileEntity : ProfileBase, IEntity
    {
        public Guid Id { get; set; }
        public ICollection<SectionMemberEntity> VisitedSections { get; set; } = [];
        public ICollection<SectionCoachEntity> MentoredSections { get; set; } = [];
    }
}