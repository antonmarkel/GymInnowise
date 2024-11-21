using GymInnowise.Shared.Sections.Interfaces;

namespace GymInnowise.Shared.Sections.Base.Relations;

public class MembershipBase : ISectionRelation
{
    public Guid SectionId { get; set; }
    public Guid RelatedId { get; set; }
    public string Goal { get; set; } = string.Empty;
}

