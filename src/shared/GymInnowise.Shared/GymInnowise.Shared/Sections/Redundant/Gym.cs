using GymInnowise.Shared.Sections.Base;
using GymInnowise.Shared.Sections.Interfaces;

namespace GymInnowise.Shared.Sections.Redundant
{
    public class Gym : GymBase, IRedundant
    {
        public Guid PrimaryId { get; set; }
    }
}
