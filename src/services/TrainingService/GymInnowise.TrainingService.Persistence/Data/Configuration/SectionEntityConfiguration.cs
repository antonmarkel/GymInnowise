using GymInnowise.TrainingService.Persistence.Entities.Redundant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class SectionEntityConfiguration : IEntityTypeConfiguration<SectionEntity>
    {
        public void Configure(EntityTypeBuilder<SectionEntity> builder)
        {
            builder.HasKey(ent => ent.OriginalId);
            builder.Property(ent => ent.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
