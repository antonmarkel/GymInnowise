using GymInnowise.Shared.Trainings.Enums;

namespace GymInnowise.TrainingService.Persistence.Entities
{
    public class RecurrenceEntity
    {
        public Guid Id { get; set; }
        public Guid TrainingId { get; set; }
        public RecurrenceTypeEnum RecurrenceType { get; set; }
        public DaysOfWeekFlagEnum DaysOfWeek { get; set; }
    }
}