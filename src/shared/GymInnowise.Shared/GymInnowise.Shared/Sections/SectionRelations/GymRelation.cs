using GymInnowise.Shared.Sections.Base.Relations;
using GymInnowise.Shared.Sections.Interfaces;

namespace GymInnowise.Shared.Sections.SectionRelations
{
    public class GymRelation : GymRelationBase, ITimeStampedModel
    {
        public DateTime AddedOnUtc { get; set; }
    }
}
