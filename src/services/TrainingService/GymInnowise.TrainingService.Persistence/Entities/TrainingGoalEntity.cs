namespace GymInnowise.TrainingService.Persistence.Entities
{
    public class TrainingGoalEntity
    {
        public Guid GoalId { get; set; }
        public required Guid TrainingId { get; set; }
        public int Value { get; set; }
        public int Goal { get; set; }
        public bool IsCancelled { get; set; }
        public required string Description { get; set; }
    }
}
