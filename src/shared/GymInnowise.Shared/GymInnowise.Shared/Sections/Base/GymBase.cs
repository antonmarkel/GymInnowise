namespace GymInnowise.Shared.Sections.Base
{
    public abstract class GymBase
    {
        public required string Name { get; set; }
        public Guid? ThumbnailId { get; set; }
    }
}
