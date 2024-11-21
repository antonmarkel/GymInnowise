namespace GymInnowise.Shared.Sections.SectionRelations.Information.Base
{
    public abstract class RelationInformationBase
    {
        public Guid RelatedId { get; set; }
        public required string FullName { get; set; }
        public Guid? ThumbnailId { get; set; }
        public DateTime AddedOnUtc { get; set; }
    }
}
