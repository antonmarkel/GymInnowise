using GymInnowise.ApiGateway.Extensions;
using Ocelot.Middleware;

var app = WebApplication.CreateBuilder(args)
    .AddJwtServices()
    .AddOcelot()
    .AddControllers()
    .Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.UseOcelot();
app.Run();