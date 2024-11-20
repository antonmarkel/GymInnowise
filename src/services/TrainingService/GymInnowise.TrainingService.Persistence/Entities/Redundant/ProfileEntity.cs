using GymInnowise.TrainingService.Persistence.Entities.Interfaces;

namespace GymInnowise.TrainingService.Persistence.Entities.Redundant
{
    public class ProfileEntity : IRedundantEntity
    {
        public Guid OriginalId { get; set; }
        public Guid? ThumbnailId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }
}
