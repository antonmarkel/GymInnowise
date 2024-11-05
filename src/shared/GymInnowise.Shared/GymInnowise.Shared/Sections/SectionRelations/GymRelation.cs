using GymInnowise.Shared.Sections.Interfaces;

namespace GymInnowise.Shared.Sections.SectionRelations
{
    public class GymRelation : ISectionRelation
    {
        public Guid SectionId { get; set; }
        public Guid RelatedId { get; set; }
        public DateTime AddedOnUtc { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
