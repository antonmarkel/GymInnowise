using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.Shared.Reports;

namespace GymInnowise.ReportService.Perstistence.Models.Entities
{
    public class TrainingReportEntity : TrainingReport, IReportEntity
    {
        public Guid Id { get; set; }
    }
}

