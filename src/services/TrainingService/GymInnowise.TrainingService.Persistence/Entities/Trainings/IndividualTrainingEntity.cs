using GymInnowise.TrainingService.Persistence.Entities.Base;

namespace GymInnowise.TrainingService.Persistence.Entities.Trainings
{
    public class IndividualTrainingEntity : TrainingEntityBase
    {
        public Guid AccountId { get; set; }
    }
}
