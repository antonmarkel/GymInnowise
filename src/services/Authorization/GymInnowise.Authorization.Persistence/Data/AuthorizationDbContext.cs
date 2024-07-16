using GymInnowise.Authorization.Persistence.Models.Enities;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.Authorization.Persistence.Data
{
    public class AuthorizationDbContext : DbContext
    {
        public DbSet<AccountEnity> Accounts { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

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
