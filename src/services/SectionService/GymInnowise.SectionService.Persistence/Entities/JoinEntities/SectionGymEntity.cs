using GymInnowise.SectionService.Persistence.Entities.Base;

namespace GymInnowise.SectionService.Persistence.Entities.JoinEntities
{
    public class SectionGymEntity : IJoinEntity
    {
        public Guid SectionId { get; set; }
        public SectionEntity? Section { get; set; }
        public Guid GymId { get; set; }
        public GymEntity? Gym { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
