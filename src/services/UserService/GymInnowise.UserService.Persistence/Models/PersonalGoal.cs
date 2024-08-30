using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Persistence.Models
{
    public class PersonalGoal
    {
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public required string Goal { get; set; }
        public Guid? SupervisorCoach { get; set; }
        public GoalStatus Status { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? DeadLine { get; set; }
    }
}
