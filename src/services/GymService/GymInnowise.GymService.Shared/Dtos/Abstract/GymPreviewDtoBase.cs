using GymInnowise.GymService.Shared.Enums;

namespace GymInnowise.GymService.Shared.Dtos.Abstract
{
    public class GymPreviewDtoBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public List<GymTag> Tags { get; set; } = [];
    }
}
