using GymInnowise.TrainingService.Persistence.Entities.Redundant;

namespace GymInnowise.TrainingService.Persistence.Entities.Trainings
{
    public class IndividualTrainingWithCoachEntity : IndividualTrainingEntity
    {
        public required ProfileEntity Coach { get; set; }
    }
}