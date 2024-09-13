
using GymInnowise.GymService.Shared.Enums;

namespace GymInnowise.GymService.Persistence.Models.Entities
{
    public class GymEventEntity
    {
        public Guid Id { get; set; }
        public Guid GymId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? TrainingId { get; set; }
        public GymEventType EventType { get; set; }
        public string Info { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
