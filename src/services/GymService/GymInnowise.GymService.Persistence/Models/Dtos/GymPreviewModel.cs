using GymInnowise.Shared.Gym.Enums;

namespace GymInnowise.GymService.Persistence.Models.Dtos
{
    public class GymPreviewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public List<GymTag> Tags { get; set; } = [];
    }
}