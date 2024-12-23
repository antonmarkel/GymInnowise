using GymInnowise.ReportService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddLogger();
builder.AddJwtServices();
builder.Services.AddAuthorization();
builder.AddConfiguration();
builder.AddPersistence();
builder.AddServices();
builder.AddRabbitMq();
builder.AddReports();

builder.Services.AddControllers();

var app = builder.Build();
app.UseGlobalExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();