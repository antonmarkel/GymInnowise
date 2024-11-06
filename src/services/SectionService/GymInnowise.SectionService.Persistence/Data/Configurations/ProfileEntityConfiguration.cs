using GymInnowise.SectionService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.SectionService.Persistence.Data.Configurations
{
    public class ProfileEntityConfiguration : IEntityTypeConfiguration<ProfileEntity>
    {
        public void Configure(EntityTypeBuilder<ProfileEntity> builder)
        {
            builder.HasKey(ent => ent.PrimaryId);
            builder.Property(ent => ent.FirstName).HasMaxLength(50);
            builder.Property(ent => ent.LastName).HasMaxLength(50);
        }
    }
}