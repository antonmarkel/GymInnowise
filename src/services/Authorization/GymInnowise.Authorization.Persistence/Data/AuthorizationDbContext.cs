using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GymInnowise.Authorization.Persistence.Data
{
    public class AuthorizationDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureRefreshTokenEntity(modelBuilder);
            ConfigureAccountEntity(modelBuilder);
            ConfigureRoleEntity(modelBuilder);
            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity { Id = Guid.NewGuid(), Role = RoleEnum.Client });
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
                entity.HasMany(e => e.Roles)
                    .WithMany(e => e.Accounts)
                    .UsingEntity(j => j.ToTable("AccountRole"));
            });
        }

        private void ConfigureRoleEntity(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<RoleEnum, string>(
                v => v.ToString(),
                v => (RoleEnum)Enum.Parse(typeof(RoleEnum), v));

            modelBuilder.Entity<RoleEntity>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasConversion(converter);

                entity.HasMany(e => e.Accounts)
                    .WithMany(e => e.Roles)
                    .UsingEntity(j => j.ToTable("AccountRole"));
            });
        }
    }
}
