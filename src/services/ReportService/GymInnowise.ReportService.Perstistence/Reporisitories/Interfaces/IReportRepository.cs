using GymInnowise.ReportService.Perstistence.Models.Interfaces;

namespace GymInnowise.ReportService.Perstistence.Reporisitories.Interfaces
{
    public interface IReportRepository<TReportEntity> where TReportEntity : class, IReportEntity
    {
        Task AddReportAsync(TReportEntity report);
        Task<TReportEntity?> GetReportAsync(Guid reportId);
    }
}
