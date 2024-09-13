using GymInnowise.GymService.Shared.Enums;

namespace GymInnowise.GymService.Persistence.Models.Entities
{
    public class GymEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string ContactInfo { get; set; }
        public GymUsageType UsageType { get; set; }
        public int MaxOccupancy { get; set; }
        public float SquareFootage { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public byte DaysAvailableMask { get; set; }
        public GymPayType PayType { get; set; }
        public decimal CostValue { get; set; }
        public List<BlockingEventEntity> BlockingEvents { get; set; } = [];
        public List<GymTag> Tags { get; set; } = [];
    }
}
