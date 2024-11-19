namespace GymInnowise.TrainingService.Persistence.Entities
{
    public class TrainingGoalEntity
    {
        public Guid GoalId { get; set; }
        public Guid TrainingGoal { get; set; }
        public int Value { get; set; }
        public int Goal { get; set; }
        public bool IsCancelled { get; set; }
        public required string Description { get; set; }
    }
}
