using GymInnowise.TrainingService.Persistence.Entities.Base;
using GymInnowise.TrainingService.Persistence.Entities.Redundant;

namespace GymInnowise.TrainingService.Persistence.Entities.Trainings
{
    public class IndividualWithCoachTrainingEntity : TrainingEntityBase
    {
        public required ProfileEntity Account { get; set; }
        public required ProfileEntity Coach { get; set; }
    }
}