using GymInnowise.ReportService.Perstistence.Data.Configuration;
using GymInnowise.ReportService.Perstistence.Models.Entities;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var trainingReportConfiguration = new TrainingReportEntityConfiguration();
            modelBuilder.ApplyConfiguration(trainingReportConfiguration);
        }
    }
}