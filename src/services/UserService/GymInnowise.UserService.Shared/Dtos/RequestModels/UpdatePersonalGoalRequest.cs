using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Shared.Dtos.RequestModels
{
    public class UpdatePersonalGoalRequest
    {
        public Guid Id { get; set; }
        public string Goal { get; set; } = string.Empty;
        public Guid? SupervisorCoach { get; set; }
        public GoalStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}
