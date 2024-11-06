using GymInnowise.Shared.Sections.Interfaces;

namespace GymInnowise.Shared.Sections.Base.Relations
{
    public class GymRelationBase : ISectionRelation
    {
        public Guid SectionId { get; set; }
        public Guid RelatedId { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
