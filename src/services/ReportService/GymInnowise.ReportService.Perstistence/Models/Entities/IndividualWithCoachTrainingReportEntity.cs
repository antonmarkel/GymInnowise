using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.Shared.Reports.Trainings;

namespace GymInnowise.ReportService.Perstistence.Models.Entities
{
    public class IndividualWithCoachTrainingReportEntity : IndividualWithCoachTrainingReport, IReportEntity
    {
        public Guid Id { get; set; }
    }
}
