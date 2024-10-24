using GymInnowise.GymService.Persistence.Data.Configuration;
using GymInnowise.GymService.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.GymService.Persistence.Data
{
    public class GymServiceDbContext : DbContext
    {
        public DbSet<GymEntity> Gyms { get; set; }
        public DbSet<GymEventEntity> GymEvents { get; set; }

        public GymServiceDbContext(DbContextOptions<GymServiceDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymEntityTypeConfiguration).Assembly);
        }
    }
}