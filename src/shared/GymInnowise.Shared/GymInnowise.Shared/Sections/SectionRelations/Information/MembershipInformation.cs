namespace GymInnowise.Shared.Sections.SectionRelations.Information
{
    public class MembershipInformation
    {
        public required string FullName { get; set; }
        public DateTime AddedOnUtc { get; set; }
        public string Goal { get; set; } = string.Empty;
    }
}
