using GymInnowise.TrainingService.Persistence.Entities.Base;
using GymInnowise.TrainingService.Persistence.Entities.Redundant;

namespace GymInnowise.TrainingService.Persistence.Entities.Trainings
{
    public class SectionTrainingEntity : TrainingEntityBase
    {
        public required SectionEntity Section { get; set; }
    }
}
