using GymInnowise.TrainingService.Persistence.Entities.Redundant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class GymEntityConfiguration : IEntityTypeConfiguration<GymEntity>
    {
        public void Configure(EntityTypeBuilder<GymEntity> builder)
        {
            builder.HasKey(ent => ent.GymId);
            builder.Property(ent => ent.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
