using GymInnowise.EmailService.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace GymInnowise.EmailService.Persistence.Data
{
    public class EmailServiceContext : DbContext
    {
        public DbSet<TemplateEntity> Templates { get; set; }

        public EmailServiceContext(DbContextOptions<EmailServiceContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTemplateEntity(modelBuilder);
        }

        private static void ConfigureTemplateEntity(ModelBuilder modelBuilder)
        {
            var dictConversion = GetDictionaryConverter();
            modelBuilder.Entity<TemplateEntity>((ent) =>
            {
                ent.HasKey(e => e.Name);
                ent.Property(e => e.Data).HasConversion(dictConversion).HasColumnType("jsonb");
                ent.Property(e => e.Data).IsRequired().HasMaxLength(100000);
                ent.Property(e => e.Subject).IsRequired().HasMaxLength(200);
            });
        }

        private static ValueConverter<HashSet<string>, string> GetDictionaryConverter()
        {
            return new ValueConverter<HashSet<string>, string>(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<HashSet<string>>(v)!);
        }
    }
}
