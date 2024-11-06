using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.SectionService.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddPersistence(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SectionDbContext>(options => options.UseNpgsql(connectionString));
            builder.Services.Scan(scan => scan
                .FromAssembliesOf(typeof(IRedundantRepository<>))
                .AddClasses(classes => classes.AssignableTo(typeof(IRedundantRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<ISectionRepository>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ISectionRelationRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }
    }
}
