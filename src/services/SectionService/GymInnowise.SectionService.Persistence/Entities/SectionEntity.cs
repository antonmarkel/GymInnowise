using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.Shared.Sections.Base;

namespace GymInnowise.SectionService.Persistence.Entities
{
    public class SectionEntity : SectionBase, IEntity
    {
        public Guid Id { get; set; }
        public ICollection<ProfileEntity> Members { get; set; } = [];
        public ICollection<ProfileEntity> Coaches { get; set; } = [];
        public ICollection<GymEntity> Gyms { get; set; } = [];
    }
}
