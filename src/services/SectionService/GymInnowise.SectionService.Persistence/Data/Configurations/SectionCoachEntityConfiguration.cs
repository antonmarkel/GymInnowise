using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.SectionService.Persistence.Data.Configurations
{
    public class SectionCoachEntityConfiguration : IEntityTypeConfiguration<SectionCoachEntity>
    {
        public void Configure(EntityTypeBuilder<SectionCoachEntity> builder)
        {
            builder.HasKey(ent => new { ent.CoachId, ent.SectionId });
            builder.HasOne(ent => ent.Coach)
                .WithMany(coach => coach.MentoredSections)
                .HasForeignKey(ent => ent.CoachId);
            builder.HasOne(ent => ent.Section)
                .WithMany(section => section.Coaches)
                .HasForeignKey(ent => ent.SectionId);
            builder.Property(ent => ent.Notes).HasMaxLength(250);
        }
    }
}
