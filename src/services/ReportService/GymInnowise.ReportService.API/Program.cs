using GymInnowise.ReportService.API.Extensions;
using GymInnowise.ReportService.Perstistence.Models.Entities;
using GymInnowise.Shared.Reports;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddReportServices<TrainingReport, TrainingReportEntity>();

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