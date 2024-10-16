using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.Shared.Reports.Interfaces;
using OneOf;
using OneOf.Types;

namespace GymInnowise.ReportService.Logic.Interfaces
{
    public interface IReportService<TReport, TReportEntity>
        where TReport : IReport
        where TReportEntity : TReport, IReportEntity
    {
        Task CreateReportAsync(TReport report, Guid reportId);
        Task<OneOf<TReport, NotFound>> GetReportAsync(Guid reportId);
    }
}
