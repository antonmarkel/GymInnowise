namespace GymInnowise.Shared.Sections.Base
{
    public class ProfileBase
    {
        public Guid AccountId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Guid? ThumbnailId { get; set; }
    }
}
