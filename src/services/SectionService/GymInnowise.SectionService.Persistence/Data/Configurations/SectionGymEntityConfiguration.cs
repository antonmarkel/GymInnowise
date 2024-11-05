using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.SectionService.Persistence.Data.Configurations
{
    public class SectionGymEntityConfiguration : IEntityTypeConfiguration<SectionGymEntity>
    {
        public void Configure(EntityTypeBuilder<SectionGymEntity> builder)
        {
            builder.HasKey(ent => new { ent.GymId, ent.SectionId });
            builder.HasOne(ent => ent.Gym)
                .WithMany(gym => gym.SectionsOnBoard)
                .HasForeignKey(ent => ent.GymId);

            builder.HasOne(ent => ent.Section)
                .WithMany(section => section.Gyms)
                .HasForeignKey(ent => ent.SectionId);
            builder.Property(ent => ent.Notes).HasMaxLength(250);
        }
    }
}
