using GymInnowise.GymService.Shared.Enums;

namespace GymInnowise.GymService.Shared.Dtos.Abstract
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