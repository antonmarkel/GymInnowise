using GymInnowise.ReportService.API.Extensions;
using GymInnowise.ReportService.Perstistence.Models.Entities;
using GymInnowise.Shared.Reports.Base;
using GymInnowise.Shared.Reports.Payment;
using GymInnowise.Shared.Reports.Trainings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddConfiguration();
builder.AddPersistence();
builder.AddServices();
builder.AddReportServices<GroupTrainingReport, GroupTrainingReportEntity>();
builder.AddReportServices<IndividualTrainingReport, IndividualTrainingReportEntity>();
builder.AddReportServices<IndividualWithCoachTrainingReport, IndividualWithCoachTrainingReportEntity>();
builder.AddReportServices<BillReport, BillReportEntity>();
builder.Services.AddControllers();

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