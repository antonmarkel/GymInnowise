using GymInnowise.Shared.Gym.Enums;

namespace GymInnowise.Shared.Gym.Dtos.Abstract
{
    public abstract class GymEventDtoBase
    {
        public Guid CreatedBy { get; set; }
        public Guid? TrainingId { get; set; }
        public GymEventType EventType { get; set; }
        public string Info { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}