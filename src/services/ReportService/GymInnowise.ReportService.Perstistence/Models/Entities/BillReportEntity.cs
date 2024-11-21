using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.Shared.Reports.Payment;

namespace GymInnowise.ReportService.Perstistence.Models.Entities
{
    public class BillReportEntity : BillReport, IReportEntity
    {
        public Guid Id { get; set; }
    }
}
