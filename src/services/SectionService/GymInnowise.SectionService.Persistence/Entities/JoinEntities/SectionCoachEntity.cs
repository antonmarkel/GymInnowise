using GymInnowise.SectionService.Persistence.Entities.Base;

namespace GymInnowise.SectionService.Persistence.Entities.JoinEntities
{
    public class SectionCoachEntity : IJoinEntity
    {
        public Guid SectionId { get; set; }
        public SectionEntity? Section { get; set; }
        public Guid CoachId { get; set; }
        public ProfileEntity? Coach { get; set; }
        public DateTime DateJoinedUtc { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
