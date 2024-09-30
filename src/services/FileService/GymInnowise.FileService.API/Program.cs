using GymInnowise.FileService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddLogger();
builder.AddJwtServices();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddConfiguration();
builder.AddPersistenceServices();
builder.AddFileServices();
builder.AddValidation();

var app = builder.Build();
app.UseGlobalExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
