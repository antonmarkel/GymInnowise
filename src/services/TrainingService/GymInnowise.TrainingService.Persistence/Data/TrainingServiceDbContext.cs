using GymInnowise.TrainingService.Persistence.Data.Configuration;
using GymInnowise.TrainingService.Persistence.Entities;
using GymInnowise.TrainingService.Persistence.Entities.Base;
using GymInnowise.TrainingService.Persistence.Entities.Redundant;
using GymInnowise.TrainingService.Persistence.Entities.Trainings;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.TrainingService.Persistence.Data
{
    public class TrainingServiceDbContext : DbContext
    {
        public DbSet<GymEntity> Gyms { get; set; } = null!;
        public DbSet<ProfileEntity> Profiles { get; set; } = null!;
        public DbSet<SectionEntity> Sections { get; set; } = null!;
        public DbSet<IndividualTrainingEntity> IndividualTrainings { get; set; } = null!;
        public DbSet<IndividualWithCoachTrainingEntity> IndividualWithCoachTrainings { get; set; } = null!;
        public DbSet<SectionTrainingEntity> SectionTrainings { get; set; } = null!;
        public DbSet<RecurrenceEntity> Recurrences { get; set; } = null!;
        public DbSet<TrainingGoalEntity> TrainingGoals { get; set; } = null!;

        public TrainingServiceDbContext(DbContextOptions<TrainingServiceDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<TrainingEntityBase>();
            modelBuilder.ApplyConfigurationsFromAssembly(ConfigurationAssemblyAccessor.Assembly);
        }
    }
}
