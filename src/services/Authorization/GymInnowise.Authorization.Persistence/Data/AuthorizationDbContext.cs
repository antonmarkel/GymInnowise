using GymInnowise.Authorization.Persistence.Models.Enities;
using Microsoft.EntityFrameworkCore;

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
        }

        private void ConfigureRefreshTokenEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshTokenEntity>(entity =>
            {
                entity.ToTable("RefreshTokens");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Token)
                    .IsRequired();

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
                    .IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .IsRequired();

                entity.HasMany(e => e.Roles)
                    .WithMany(e => e.Accounts)
                    .UsingEntity(j => j.ToTable("AccountRole")); 
            });
        }

        private void ConfigureRoleEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleEntity>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.RoleName)
                    .IsRequired();

                entity.HasMany(e => e.Accounts)
                    .WithMany(e => e.Roles)
                    .UsingEntity(j => j.ToTable("AccountRole"));
            });
        }
    }
}
