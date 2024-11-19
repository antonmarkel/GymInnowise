namespace GymInnowise.TrainingService.Persistence.Entities.Redundant
{
    public class GymEntity
    {
        public Guid GymId { get; set; }
        public required string Name { get; set; }
        public Guid? ThumbnailId { get; set; }
    }
}
