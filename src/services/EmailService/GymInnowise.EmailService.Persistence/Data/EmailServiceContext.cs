using GymInnowise.EmailService.Persistence.Data.Configuration;
using GymInnowise.EmailService.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.EmailService.Persistence.Data
{
    public class EmailServiceContext : DbContext
    {
        public DbSet<EmailVerificationEntity> EmailVerifications { get; set; }

        public EmailServiceContext(DbContextOptions<EmailServiceContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmailVerificationEntityConfiguration).Assembly);
        }
    }
}
