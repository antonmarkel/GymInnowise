using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.Shared.RabbitMq.Events.Reports;
using GymInnowise.Shared.Reports.Interfaces;
using MassTransit;

namespace GymInnowise.ReportService.Logic.Consumers
{
    public class ReportConsumer<TReport, TReportEntity> : IConsumer<CreateReportEvent<TReport>>
        where TReport : IReport
        where TReportEntity : class, TReport, IReportEntity
    {
        private readonly IReportService<TReport, TReportEntity> _reportService;

        public ReportConsumer(IReportService<TReport, TReportEntity> reportService)
        {
            _reportService = reportService;
        }

        public async Task Consume(ConsumeContext<CreateReportEvent<TReport>> context)
        {
            await _reportService.CreateReportAsync(context.Message.Report, context.Message.Id);
        }
    }
}
