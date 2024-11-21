using GymInnowise.UserService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddLogger();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.AddJwtServices();
builder.AddPersistenceServices();
builder.AddUserServices();
builder.AddRabbitMq();
builder.AddValidation();

var app = builder.Build();
app.UseGlobalExceptionHandler();
await app.MigrateDatabaseAsync();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
