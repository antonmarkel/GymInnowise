namespace GymInnowise.Shared.Sections.Interfaces
{
    public interface ISectionRelation
    {
        Guid SectionId { get; set; }
        Guid RelatedId { get; set; }
        DateTime AddedOnUtc { get; set; }
    }
}
