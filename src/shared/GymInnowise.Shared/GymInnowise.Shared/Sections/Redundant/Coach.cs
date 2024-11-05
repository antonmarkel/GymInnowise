using GymInnowise.Shared.Sections.Base;
using GymInnowise.Shared.Sections.Interfaces;

namespace GymInnowise.Shared.Sections.Redundant
{
    public class Coach : ProfileBase, IRedundant
    {
        public Guid PrimaryId { get; set; }
    }
}
