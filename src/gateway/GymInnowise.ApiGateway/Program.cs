using GymInnowise.ApiGateway.Extensions;
using Ocelot.Middleware;

var app = WebApplication.CreateBuilder(args)
    .AddJwtServices()
    .AddOcelot()
    .Build();

app.UseAuthentication();
app.UseAuthorization();
await app.UseOcelot();
app.Run();