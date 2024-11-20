using GymInnowise.Shared.Trainings.Enums;
using GymInnowise.TrainingService.Persistence.Entities.Redundant;

namespace GymInnowise.TrainingService.Persistence.Entities.Base
{
    public abstract class TrainingEntityBase
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public Guid? GymId { get; set; }
        public GymEntity? Gym { get; set; }
        public DateTime DateStartUtc { get; set; }
        public DateTime DateEndUtc { get; set; }
        public RecurrenceEntity? Recurrence { get; set; }
        public TrainingStatusEnum Status { get; set; }
        public Guid? ReportId { get; set; }
        public ICollection<TrainingGoalEntity> Goals { get; set; } = [];
    }
}