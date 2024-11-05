using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.SectionService.Persistence.Data.Configurations
{
    internal class SectionMemberEntityConfiguration : IEntityTypeConfiguration<SectionMemberEntity>
    {
        public void Configure(EntityTypeBuilder<SectionMemberEntity> builder)
        {
            builder.HasKey(ent => new { ent.Member, ent.SectionId });
            builder.HasOne(ent => ent.Member)
                .WithMany(member => member.VisitedSections)
                .HasForeignKey(ent => ent.RelatedId);
            builder.HasOne(ent => ent.Section)
                .WithMany(section => section.Members)
                .HasForeignKey(ent => ent.SectionId);
            builder.Property(ent => ent.Goal).HasMaxLength(250);
        }
    }
}
