using GymInnowise.Authorization.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.Authorization.Persistence.Data.Configuration
{
    public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> entity)
        {
            entity.ToTable("RefreshTokens");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Token).HasMaxLength(150);
            entity.HasIndex(e => e.Token).IsUnique();
            entity.HasOne(e => e.Account)
                .WithMany()
                .IsRequired();
        }
    }
}
