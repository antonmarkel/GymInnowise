namespace GymInnowise.TrainingService.Persistence.Entities.Trainings
{
    public class IndividualTrainingWithCoachEntity : IndividualTrainingEntity
    {
        public Guid Coach { get; set; }
    }
}
