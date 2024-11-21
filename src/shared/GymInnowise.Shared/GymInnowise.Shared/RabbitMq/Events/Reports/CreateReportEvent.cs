using GymInnowise.Shared.Reports.Interfaces;

namespace GymInnowise.Shared.RabbitMq.Events.Reports
{
    public class CreateReportEvent<TReport> where TReport : IReport
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required TReport Report { get; set; }
    }
}
