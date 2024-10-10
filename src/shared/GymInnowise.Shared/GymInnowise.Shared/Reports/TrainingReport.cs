using GymInnowise.Shared.Reports.Interfaces;

namespace GymInnowise.Shared.Reports
{
    public class TrainingReport : IReport
    {
        //TODO: Goals
        public required string[] Participants { get; set; }
        public string[] Coaches { get; set; } = [];
        public string? Section { get; set; }
        public string? Gym { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public Guid FileId { get; set; }
    }
}
