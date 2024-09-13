using GymInnowise.GymService.Shared.Enums;

namespace GymInnowise.GymService.Shared.Dtos.Responses
{
    public class GetGymPreviewResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public List<GymTag> Tags { get; set; } = [];
    }
}
