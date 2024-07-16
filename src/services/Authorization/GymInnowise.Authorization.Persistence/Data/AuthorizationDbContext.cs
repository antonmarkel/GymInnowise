using GymInnowise.Authorization.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.Authorization.Persistence.Data
{
    public class AuthorizationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public AuthorizationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=GymInnowise;Username=postgres;Password=MADL");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Role>().HasData(new Role { RoleName = "Client", Id = Guid.NewGuid() });
        }

    }
}
