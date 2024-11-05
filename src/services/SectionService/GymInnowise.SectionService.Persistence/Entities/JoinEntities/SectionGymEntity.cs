using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.Shared.Sections.SectionRelations;

namespace GymInnowise.SectionService.Persistence.Entities.JoinEntities
{
    public class SectionGymEntity : GymRelation, IJoinEntity
    {
        public SectionEntity? Section { get; set; }
        public GymEntity? Gym { get; set; }
    }
}
