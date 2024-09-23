using GymInnowise.Authorization.Persistence.Models.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace GymInnowise.Authorization.Persistence.Data
{
    public class AuthorizationDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureRefreshTokenEntity(modelBuilder);
            ConfigureAccountEntity(modelBuilder);
        }

        private void ConfigureRefreshTokenEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshTokenEntity>(entity =>
            {
                entity.ToTable("RefreshTokens");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).HasMaxLength(150);
                entity.HasIndex(e => e.Token).IsUnique();
                entity.HasOne(e => e.Account)
                    .WithMany()
                    .IsRequired();
            });
        }

        private void ConfigureAccountEntity(ModelBuilder modelBuilder)
        {
            var rolesConverter = GetRolesConverter();
            modelBuilder.Entity<AccountEntity>(entity =>
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
                    .HasConversion(rolesConverter);
            });
        }

        private static ValueConverter<List<string>, string> GetRolesConverter()
        {
            return new ValueConverter<List<string>, string>(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v)!);
        }
    }
}