using GymInnowise.TrainingService.Persistence.Entities.Redundant;

namespace GymInnowise.TrainingService.Persistence.Entities.Trainings
{
    public class IndividualWithCoachTrainingEntity : IndividualTrainingEntity
    {
        public required ProfileEntity Coach { get; set; }
    }
}