using GymInnowise.ReportService.API.Controllers.Base;
using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Perstistence.Models.Entities;
using GymInnowise.Shared.Reports.Trainings;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.ReportService.API.Controllers.Trainings
{
    [ApiController]
    [Route("api/reports/individual-with-coach-training")]
    public class
        IndividualWithCoachTrainingReportController : ReportControllerBase<IndividualWithCoachTrainingReport,
        IndividualWithCoachTrainingReportEntity>
    {
        public IndividualWithCoachTrainingReportController(
            IReportService<IndividualWithCoachTrainingReport, IndividualWithCoachTrainingReportEntity> reportService,
            IReportFileGenerator<IndividualWithCoachTrainingReport> fileGenerator) : base(reportService, fileGenerator)
        {
        }
    }
}