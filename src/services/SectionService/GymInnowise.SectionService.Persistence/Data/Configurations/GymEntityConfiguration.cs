using GymInnowise.SectionService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.SectionService.Persistence.Data.Configurations
{
    public class GymEntityConfiguration : IEntityTypeConfiguration<GymEntity>
    {
        public void Configure(EntityTypeBuilder<GymEntity> builder)
        {
            builder.HasKey(ent => ent.GymId);
            builder.Property(ent => ent.Name).HasMaxLength(100);
        }
    }
}
