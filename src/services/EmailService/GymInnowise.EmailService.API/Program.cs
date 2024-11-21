using GymInnowise.EmailService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddLogger();
builder.AddJwtServices();
builder.AddConfiguration();
builder.AddServices();
builder.AddRabbitMq();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
