using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.Redundant;

namespace GymInnowise.SectionService.Persistence.Entities
{
    public class ProfileEntity : Profile, IEntity
    {
        public ICollection<SectionMemberEntity> VisitedSections { get; set; } = [];
        public ICollection<SectionCoachEntity> MentoredSections { get; set; } = [];
    }
}