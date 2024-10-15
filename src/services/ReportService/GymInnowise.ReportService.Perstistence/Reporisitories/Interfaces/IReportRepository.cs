using GymInnowise.ReportService.Perstistence.Models.Base;

namespace GymInnowise.ReportService.Perstistence.Reporisitories.Interfaces
{
    public interface IReportRepository<TReportEntity> where TReportEntity : ReportEntityBase
    {
        Task AddReportAsync(TReportEntity report);
        Task<TReportEntity?> GetReportAsync(Guid reportId);
    }
}
