using GymInnowise.TrainingService.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class TrainingEntityBaseConfiguration : IEntityTypeConfiguration<TrainingEntityBase>
    {
        public virtual void Configure(EntityTypeBuilder<TrainingEntityBase> builder)
        {
            builder.HasKey(ent => ent.Id);
            builder.Property(ent => ent.Title).HasMaxLength(255);
            builder.HasOne(ent => ent.Gym);
            builder.HasOne(ent => ent.Recurrence);
            builder.Property(ent => ent.Status).HasConversion<string>();
            builder.HasMany(ent => ent.Goals);
        }
    }
}
