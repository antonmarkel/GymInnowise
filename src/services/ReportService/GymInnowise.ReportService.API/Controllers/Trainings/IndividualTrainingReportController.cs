using GymInnowise.ReportService.API.Controllers.Base;
using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Perstistence.Models.Entities;
using GymInnowise.Shared.Reports.Trainings;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.ReportService.API.Controllers.Trainings
{
    [ApiController]
    [Route("api/reports/individual-training")]
    public class
        IndividualTrainingReportController : ReportControllerBase<IndividualTrainingReport,
        IndividualTrainingReportEntity>
    {
        public IndividualTrainingReportController(
            IReportService<IndividualTrainingReport, IndividualTrainingReportEntity> reportService,
            IReportFileGenerator<IndividualTrainingReport> fileGenerator) : base(reportService, fileGenerator)
        {
        }
    }
}