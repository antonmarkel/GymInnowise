using GymInnowise.TrainingService.Persistence.Entities.Base;
using GymInnowise.TrainingService.Persistence.Entities.Redundant;

namespace GymInnowise.TrainingService.Persistence.Entities.Trainings
{
    public class IndividualTrainingEntity : TrainingEntityBase
    {
        public required ProfileEntity Account { get; set; }
    }
}