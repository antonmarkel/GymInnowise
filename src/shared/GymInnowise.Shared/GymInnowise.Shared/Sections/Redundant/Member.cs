using GymInnowise.Shared.Sections.Base;
using GymInnowise.Shared.Sections.Interfaces;

namespace GymInnowise.Shared.Sections.Redundant
{
    public class Member : ProfileBase, IRedundant
    {
        public Guid PrimaryId { get; set; }
    }
}
