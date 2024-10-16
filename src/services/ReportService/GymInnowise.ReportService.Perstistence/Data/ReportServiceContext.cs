using GymInnowise.ReportService.Perstistence.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.ReportService.Perstistence.Data
{
    public class ReportServiceContext : DbContext
    {
        public DbSet<IndividualTrainingReportEntity> IndividualTrainingReports { get; set; }
        public DbSet<IndividualWithCoachTrainingReportEntity> IndividualWithCoachTrainingReports { get; set; }
        public DbSet<GroupTrainingReportEntity> GroupTrainingReports { get; set; }
        public DbSet<BillReportEntity> BillReports { get; set; }

        public ReportServiceContext(DbContextOptions<ReportServiceContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}