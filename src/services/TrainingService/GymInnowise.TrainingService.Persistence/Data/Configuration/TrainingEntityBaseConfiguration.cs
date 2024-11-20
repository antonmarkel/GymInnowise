using GymInnowise.TrainingService.Persistence.Entities;
using GymInnowise.TrainingService.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public abstract class TrainingEntityBaseConfiguration<TTrainingEntity> : IEntityTypeConfiguration<TTrainingEntity>
        where TTrainingEntity : TrainingEntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TTrainingEntity> builder)
        {
            builder.Property(ent => ent.Title).HasMaxLength(255);
            builder.HasOne(ent => ent.Gym).WithMany().HasForeignKey(ent => ent.GymId);
            builder.Property(ent => ent.Status).HasConversion<string>();
            builder.HasMany(ent => ent.Goals).WithOne().HasForeignKey(goal => goal.TrainingId);
            builder.HasOne(ent => ent.Recurrence)
                .WithOne()
                .HasForeignKey<RecurrenceEntity>(rec => rec.TrainingId)
                .IsRequired(false);
        }
    }
}