using GymInnowise.ReportService.Perstistence.Models.Base;

namespace GymInnowise.ReportService.Perstistence.Reporisitories.Interfaces
{
    public interface IReportRepository<TReportEntity> where TReportEntity : ReportEntityBase
    {
        Task AddAsync(TReportEntity report);
        Task<TReportEntity?> GetAsync(Guid reportId);
    }
}
