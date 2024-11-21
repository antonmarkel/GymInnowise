using GymInnowise.ReportService.Perstistence.Data;
using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.ReportService.Perstistence.Reporisitories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.ReportService.Perstistence.Reporisitories
{
    public class ReportRepository<TReportEntity> : IReportRepository<TReportEntity>
        where TReportEntity : class, IReportEntity
    {
        private readonly ReportServiceContext _context;

        public ReportRepository(ReportServiceContext context)
        {
            _context = context;
        }

        public async Task AddReportAsync(TReportEntity report)
        {
            await _context.Set<TReportEntity>().AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task<TReportEntity?> GetReportAsync(Guid reportId)
        {
            return await _context.Set<TReportEntity>().SingleOrDefaultAsync(rep => rep.Id == reportId);
        }

        public async Task UpdateReportAsync(TReportEntity reportEntity)
        {
            _context.Set<TReportEntity>().Update(reportEntity);
            await _context.SaveChangesAsync();
        }
    }
}