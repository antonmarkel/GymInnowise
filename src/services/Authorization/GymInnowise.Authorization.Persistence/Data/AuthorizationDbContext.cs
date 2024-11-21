using GymInnowise.Authorization.Persistence.Data.Configuration;
using GymInnowise.Authorization.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.Authorization.Persistence.Data
{
    public class AuthorizationDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        public DbSet<VerificationEntity> Verifications { get; set; }

        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountEntityConfiguration).Assembly);
        }
    }
}