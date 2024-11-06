using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.Base;

namespace GymInnowise.SectionService.Persistence.Entities
{
    public class SectionEntity : SectionBase, IEntity
    {
        public Guid PrimaryId { get; set; }
        public ICollection<SectionMemberEntity> Members { get; set; } = [];
        public ICollection<SectionCoachEntity> Coaches { get; set; } = [];
        public ICollection<SectionGymEntity> Gyms { get; set; } = [];
    }
}
