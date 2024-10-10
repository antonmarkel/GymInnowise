using GymInnowise.ReportService.Perstistence.Data;
using GymInnowise.ReportService.Perstistence.Models;
using GymInnowise.ReportService.Perstistence.Reporisitories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.ReportService.Perstistence.Reporisitories
{
    public class TrainingReportRepository(ReportServiceContext _context) : IReportRepository<TrainingReportEntity>
    {
        public async Task AddAsync(TrainingReportEntity report)
        {
            await _context.TrainingReports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task<TrainingReportEntity?> GetAsync(Guid reportId)
        {
            return await _context.TrainingReports.SingleOrDefaultAsync(rep => rep.Id == reportId);
        }
    }
}
