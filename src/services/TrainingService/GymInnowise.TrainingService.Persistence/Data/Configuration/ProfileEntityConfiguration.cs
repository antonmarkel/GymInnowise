using GymInnowise.TrainingService.Persistence.Entities.Redundant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class ProfileEntityConfiguration : IEntityTypeConfiguration<ProfileEntity>
    {
        public void Configure(EntityTypeBuilder<ProfileEntity> builder)
        {
            builder.HasKey(ent => ent.AccountId);
            builder.Property(ent => ent.FirstName)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(ent => ent.LastName)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(ent => ent.Email)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
