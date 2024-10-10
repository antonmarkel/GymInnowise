using GymInnowise.ReportService.Perstistence.Models;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.ReportService.Perstistence.Data
{
    public class ReportServiceContext : DbContext
    {
        public DbSet<TrainingReportEntity> TrainingReports { get; set; }

        public ReportServiceContext(DbContextOptions<ReportServiceContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
