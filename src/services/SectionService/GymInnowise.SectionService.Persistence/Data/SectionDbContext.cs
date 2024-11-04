using GymInnowise.SectionService.Persistence.Data.Configurations;
using GymInnowise.SectionService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.SectionService.Persistence.Data
{
    public class SectionDbContext : DbContext
    {
        public SectionDbContext(DbContextOptions<SectionDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymEntityConfiguration).Assembly);
        }

        public DbSet<SectionEntity> Sections { get; set; }
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<GymEntity> Gyms { get; set; }
    }
}