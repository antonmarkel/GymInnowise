using GymInnowise.Shared.User.Enums;

namespace GymInnowise.Shared.User.Dtos.ResponseModels.Gets
{
    public class GetPersonalGoalResponse
    {
        public Guid Owner { get; set; }
        public string Goal { get; set; } = string.Empty;
        public Guid? SupervisorCoach { get; set; }
        public GoalStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}