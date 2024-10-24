using GymInnowise.GymService.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.GymService.Persistence.Data.Configuration
{
    public class GymEntityTypeConfiguration : IEntityTypeConfiguration<GymEntity>
    {
        public void Configure(EntityTypeBuilder<GymEntity> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name).HasMaxLength(100).IsRequired();
            builder.Property(g => g.Address).HasMaxLength(200).IsRequired();
            builder.Property(g => g.ContactInfo).HasMaxLength(200).IsRequired();
            builder.Property(g => g.UsageType).HasConversion<string>().IsRequired();
            builder.Property(g => g.PayType).HasConversion<string>().IsRequired();
            builder.Property(g => g.Tags).IsRequired();
        }
    }
}
