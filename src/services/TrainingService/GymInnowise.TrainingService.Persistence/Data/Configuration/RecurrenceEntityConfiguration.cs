using GymInnowise.TrainingService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class RecurrenceEntityConfiguration : IEntityTypeConfiguration<RecurrenceEntity>
    {
        public void Configure(EntityTypeBuilder<RecurrenceEntity> builder)
        {
            builder.HasKey(ent => ent.TrainingId);
            builder.Property(ent => ent.RecurrenceType).HasConversion<string>();
            builder.Property(ent => ent.DaysOfWeek).HasConversion<byte>();
        }
    }
}
