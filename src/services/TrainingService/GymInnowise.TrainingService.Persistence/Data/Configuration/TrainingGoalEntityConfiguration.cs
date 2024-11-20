using GymInnowise.TrainingService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class TrainingGoalEntityConfiguration : IEntityTypeConfiguration<TrainingGoalEntity>
    {
        public void Configure(EntityTypeBuilder<TrainingGoalEntity> builder)
        {
            builder.HasKey(ent => new { ent.GoalId, ent.TrainingId });
            builder.Property(ent => ent.Description).HasMaxLength(255);
        }
    }
}