using GymInnowise.GymService.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.GymService.Persistence.Data.Configuration
{
    public class GymEventEntityTypeConfiguration : IEntityTypeConfiguration<GymEventEntity>
    {
        public void Configure(EntityTypeBuilder<GymEventEntity> builder)
        {
            builder.HasKey(ev => ev.Id);
            builder.HasIndex(ev => ev.GymId);
            builder.Property(ev => ev.Info).HasMaxLength(250).IsRequired();
            builder.Property(ev => ev.EventType).HasConversion<string>();
        }
    }
}
