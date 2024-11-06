namespace GymInnowise.Shared.Sections.Base
{
    public abstract class ProfileBase
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Guid? ThumbnailId { get; set; }
        public required string Role { get; set; }
    }
}
