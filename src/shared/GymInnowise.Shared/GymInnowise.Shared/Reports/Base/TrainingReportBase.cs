using GymInnowise.Shared.Reports.Interfaces;

namespace GymInnowise.Shared.Reports.Base
{
    public abstract class TrainingReportBase : IReport
    {
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public Dictionary<string, bool> Goals { get; set; } = [];
        public string? Gym { get; set; }
        public DateTime DateStampUtc { get; set; }
    }
}
