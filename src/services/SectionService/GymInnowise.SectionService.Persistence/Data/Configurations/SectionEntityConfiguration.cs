using GymInnowise.SectionService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.SectionService.Persistence.Data.Configurations
{
    public class SectionEntityConfiguration : IEntityTypeConfiguration<SectionEntity>
    {
        public void Configure(EntityTypeBuilder<SectionEntity> builder)
        {
            builder.HasKey(ent => ent.PrimaryId);
            builder.Property(ent => ent.Name).HasMaxLength(100);
            builder.Property(ent => ent.Description).HasMaxLength(1000);
        }
    }
}