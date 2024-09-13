using GymInnowise.GymService.Shared.Enums;

namespace GymInnowise.GymService.Shared.Dtos.Requests
{
    public class GymUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public int MaxOccupancy { get; set; }
        public float SquareFootage { get; set; }
        public GymUsageType UsageType { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public GymPayType PayType { get; set; }
        public decimal CostValue { get; set; }
        public List<GymTag> Tags { get; set; } = [];
    }
}
