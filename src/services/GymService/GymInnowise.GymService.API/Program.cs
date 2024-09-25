using GymInnowise.GymService.API.Extensions;
using GymInnowise.GymService.API.Swagger.Filters;
using GymInnowise.GymService.Logic.Mappings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.AddLogger();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SchemaFilter<TimeSpanSchemaFilter>());
builder.AddJwtServices();
builder.AddValidationServices();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.AddGymServices();
builder.AddPersistenceServices();

var app = builder.Build();
app.UseGlobalExceptionHandler();
app.UseSerilogRequestLogging();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();