namespace GymInnowise.SectionService.Configuration
{
    public class SectionDataRestrictions
    {
        public int SectionNameMaxLength { get; } = 100;
        public int SectionAboutMaxLength { get; } = 250;
        public int SectionCoachNotesMaxLength { get; } = 250;
        public int SectionGymNotesMaxLength { get; } = 250;
        public int SectionMemberGoalMaxLength { get; } = 250;
        public int MaxTagAmount { get; } = 10;
        public int TagMaxLength { get; } = 50;
    }
}
