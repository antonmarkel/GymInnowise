using GymInnowise.ApiGateway.Extensions;
using Ocelot.Middleware;

var app = WebApplication.CreateBuilder(args)
    .AddJwtServices()
    .AddOcelot()
    .Build();

await app.UseOcelot();
app.Run();