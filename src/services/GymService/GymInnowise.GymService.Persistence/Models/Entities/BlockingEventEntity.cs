
namespace GymInnowise.GymService.Persistence.Models.Entities
{
    public class BlockingEventEntity
    {
        public Guid Id { get; set; }
        public Guid GymId { get; set; }
        public Guid CreatedBy { get; set; }
        public string Reason { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
