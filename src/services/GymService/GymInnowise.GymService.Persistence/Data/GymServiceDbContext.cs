using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

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
            ConfigureGymEntity(modelBuilder);
            ConfigureBlockingEventEntity(modelBuilder);
        }

        private void ConfigureGymEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GymEntity>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Name).HasMaxLength(100).IsRequired();
                entity.Property(g => g.Address).HasMaxLength(200).IsRequired();
                entity.Property(g => g.ContactInfo).HasMaxLength(200).IsRequired();
                entity.HasMany(g => g.BlockingEvents).WithOne().HasForeignKey(ev => ev.GymId);
                entity.Property(g => g.UsageType).HasConversion<string>().IsRequired();
                entity.Property(g => g.PayType).HasConversion<string>().IsRequired();
                entity.Property(g => g.Tags).HasConversion(GetGymTagConverter());
            });
        }

        private void ConfigureBlockingEventEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GymEventEntity>(entity =>
            {
                entity.HasKey(ev => ev.Id);
                entity.Property(ev => ev.Info).HasMaxLength(250).IsRequired();
                entity.Property(ev => ev.EventType).HasConversion<string>();
            });
        }

        private static ValueConverter<List<GymTag>, string> GetGymTagConverter()
        {
            return new ValueConverter<List<GymTag>, string>(
                v => JsonConvert.SerializeObject(
                    v.Select(v => v.ToString())),
                v => JsonConvert.DeserializeObject<List<string>>(v)!
                    .Select(s => (GymTag)Enum.Parse(typeof(GymTag), s)).ToList());
        }
    }
}