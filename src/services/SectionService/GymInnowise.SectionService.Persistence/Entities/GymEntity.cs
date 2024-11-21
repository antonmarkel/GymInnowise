using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.Redundant;

namespace GymInnowise.SectionService.Persistence.Entities
{
    public class GymEntity : Gym, IEntity
    {
        public ICollection<SectionGymEntity> SectionsOnBoard { get; set; } = [];
    }
}
