using GymInnowise.ReportService.Perstistence.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.ReportService.Perstistence.Data.Configuration
{
    internal class TrainingReportEntityConfiguration : IEntityTypeConfiguration<TrainingReportEntity>
    {
        public void Configure(EntityTypeBuilder<TrainingReportEntity> builder)
        {
            builder.HasKey(ent => ent.Id);
        }
    }
}
