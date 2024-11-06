using GymInnowise.SectionService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder
    .AddBase()
    .AddConfiguration()
    .AddLogger()
    .AddValidation()
    .AddJwtServices()
    .AddPersistence()
    .AddMappers()
    .AddMediatr()
    .AddRabbitMq();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();