using GymInnowise.Shared.Trainings.Enums;

namespace GymInnowise.TrainingService.Persistence.Entities.Base
{
    public abstract class TrainingEntityBase
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public Guid? GymId { get; set; }
        public DateTime DateStartUtc { get; set; }
        public DateTime DateEndUtc { get; set; }
        public RecurrenceEntity? Recurrence { get; set; }
        public TrainingStatusEnum Status { get; set; }
        public Guid? ReportId { get; set; }
        private ICollection<TrainingGoalEntity> Goals { get; set; } = [];
    }
}