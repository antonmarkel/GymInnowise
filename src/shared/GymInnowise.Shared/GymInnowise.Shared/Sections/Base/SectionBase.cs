
namespace GymInnowise.Shared.Sections.Base
{
    public class SectionBase
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal CostPerTraining { get; set; }
        public string[] Tags { get; set; } = [];
    }
}
