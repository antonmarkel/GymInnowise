using GymInnowise.EmailService.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.EmailService.Persistence.Data.Configuration
{
    public class EmailVerificationEntityConfiguration : IEntityTypeConfiguration<EmailVerificationEntity>
    {
        public void Configure(EntityTypeBuilder<EmailVerificationEntity> ent)
        {
            ent.HasKey(e => e.Id);
            ent.Property(e => e.CreatedAt).IsRequired();
            ent.Property(e => e.ExpireAt).IsRequired();
        }
    }
}
