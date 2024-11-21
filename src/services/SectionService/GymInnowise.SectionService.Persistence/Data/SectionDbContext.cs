using GymInnowise.SectionService.Persistence.Data.Configurations;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.SectionService.Persistence.Data
{
    public class SectionDbContext : DbContext
    {
        public DbSet<SectionEntity> Sections { get; set; }
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<GymEntity> Gyms { get; set; }

        public DbSet<SectionGymEntity> SectionGyms { get; set; }
        public DbSet<SectionMemberEntity> SectionMembers { get; set; }
        public DbSet<SectionCoachEntity> SectionCoaches { get; set; }

        public SectionDbContext(DbContextOptions<SectionDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymEntityConfiguration).Assembly);
        }
    }
}