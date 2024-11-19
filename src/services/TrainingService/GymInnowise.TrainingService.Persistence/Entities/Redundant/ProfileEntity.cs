namespace GymInnowise.TrainingService.Persistence.Entities.Redundant
{
    public class ProfileEntity
    {
        public Guid AccountId { get; set; }
        public Guid? ThumbnailId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }
}
