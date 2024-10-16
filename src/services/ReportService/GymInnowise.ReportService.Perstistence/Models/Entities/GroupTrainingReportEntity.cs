using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.Shared.Reports.Trainings;

namespace GymInnowise.ReportService.Perstistence.Models.Entities
{
    public class GroupTrainingReportEntity : GroupTrainingReport, IReportEntity
    {
        public Guid Id { get; set; }
    }
}
