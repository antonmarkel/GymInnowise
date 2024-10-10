using GymInnowise.ReportService.Perstistence.Models.Base;

namespace GymInnowise.ReportService.Perstistence.Models.Entities
{
    public class TrainingReportEntity : ReportEntityBase
    {
        public required string[] Participants { get; set; }
        public string[] Coaches { get; set; } = [];
        public string? Section { get; set; }
        public string? Gym { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
    }
}

