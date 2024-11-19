using GymInnowise.TrainingService.Persistence.Entities.Base;

namespace GymInnowise.TrainingService.Persistence.Entities.Trainings
{
    public class SectionTrainingEntity : TrainingEntityBase
    {
        public Guid? SectionId { get; set; }
    }
}
