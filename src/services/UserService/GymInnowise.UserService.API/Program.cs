using GymInnowise.UserService.API.Authorization;
using GymInnowise.UserService.API.Extensions;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyNames.OwnerOrAdmin, policy => policy.Requirements.Add(new OwnerOrAdminRequirement()));
});
builder.AddAutherizationServices();
builder.AddJwtServices();
builder.AddPersistenceServices();
builder.AddUserServices();
builder.AddValidation();

var app = builder.Build();
await app.MigrateDatabaseAsync();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
