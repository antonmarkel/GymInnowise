using GymInnowise.GymService.Logic.Interfaces;
using GymInnowise.GymService.Logic.Services;
using GymInnowise.GymService.Persistence.Data;
using GymInnowise.GymService.Persistence.Repositories.Implementations;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.GymService.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddGymServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IGymService, Logic.Services.GymService>();
            builder.Services.AddScoped<IGymEventService, GymEventService>();
        }

        public static void AddPersistenceServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<GymServiceDbContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddScoped<IGymRepository, GymRepository>();
            builder.Services.AddScoped<IGymEventRepository, GymEventRepository>();
        }
    }
}
