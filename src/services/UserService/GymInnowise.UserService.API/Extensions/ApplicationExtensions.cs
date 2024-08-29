using GymInnowise.UserService.Configuration.Data;
using GymInnowise.UserService.Persistence.Data;

namespace GymInnowise.UserService.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddPersistenceServices(this IHostApplicationBuilder builder)
        {
            builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
            builder.Services.AddSingleton<DataContext>();
        }
    }
}