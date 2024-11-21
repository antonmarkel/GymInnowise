using GymInnowise.Authorization.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.AddLogger();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddJwtServices();
builder.Services.AddHttpContextAccessor();
builder.AddVerificationService();
builder.AddRabbitMq();
builder.AddValidation();
builder.AddPersistenceServices();

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseGlobalExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();