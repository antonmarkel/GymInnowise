using GymInnowise.Shared.Gym.Enums;

namespace GymInnowise.Shared.Gym.Dtos.Abstract
{
    public abstract class GymPreviewDtoBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public List<GymTag> Tags { get; set; } = [];
    }
}