using GymInnowise.ReportService.Perstistence.Models.Base;
using GymInnowise.Shared.Reports.Interfaces;
using OneOf;
using OneOf.Types;

namespace GymInnowise.ReportService.Logic.Interfaces
{
    public interface IReportService<TReport, TReportEntity>
        where TReport : IReport
        where TReportEntity : ReportEntityBase
    {
        Task CreateReportAsync(TReport report);
        Task<OneOf<TReport, NotFound>> GetReportAsync(Guid reportId);
    }
}
