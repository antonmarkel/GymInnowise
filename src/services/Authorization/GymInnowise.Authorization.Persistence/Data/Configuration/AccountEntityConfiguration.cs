using GymInnowise.Authorization.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace GymInnowise.Authorization.Persistence.Data.Configuration
{
    public class AccountEntityConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> entity)
        {
            entity.ToTable("Accounts");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(a => a.Roles)
                .HasConversion(GetRolesConverter());
        }

        private static ValueConverter<List<string>, string> GetRolesConverter()
        {
            return new ValueConverter<List<string>, string>(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v)!);
        }
    }
}
