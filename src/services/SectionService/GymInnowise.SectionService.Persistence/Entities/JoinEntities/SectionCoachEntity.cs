using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.Shared.Sections.SectionRelations;

namespace GymInnowise.SectionService.Persistence.Entities.JoinEntities
{
    public class SectionCoachEntity : Mentorship, IJoinEntity
    {
        public SectionEntity? Section { get; set; }
        public ProfileEntity? Coach { get; set; }
    }
}
