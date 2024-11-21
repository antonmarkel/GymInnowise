using GymInnowise.Shared.Sections.Base.Relations;
using GymInnowise.Shared.Sections.Interfaces;

namespace GymInnowise.Shared.Sections.SectionRelations
{
    public class Mentorship : MentorshipBase, ITimeStampedModel
    {
        public DateTime AddedOnUtc { get; set; }
    }
}
