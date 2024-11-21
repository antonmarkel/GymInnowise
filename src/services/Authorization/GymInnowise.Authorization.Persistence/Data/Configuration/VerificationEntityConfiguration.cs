using GymInnowise.Authorization.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.Authorization.Persistence.Data.Configuration
{
    public class VerificationEntityConfiguration : IEntityTypeConfiguration<VerificationEntity>
    {
        public void Configure(EntityTypeBuilder<VerificationEntity> entity)
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AccountId).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.ExpireAt).IsRequired();
        }
    }
}