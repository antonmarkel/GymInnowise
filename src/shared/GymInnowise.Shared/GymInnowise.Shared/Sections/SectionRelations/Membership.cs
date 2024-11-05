using GymInnowise.Shared.Sections.Interfaces;

namespace GymInnowise.Shared.Sections.SectionRelations
{
    public class Membership : ISectionRelation
    {
        public Guid RelatedId { get; set; }
        public Guid SectionId { get; set; }
        public DateTime AddedOnUtc { get; set; }
        public string Goal { get; set; } = string.Empty;
    }
}
