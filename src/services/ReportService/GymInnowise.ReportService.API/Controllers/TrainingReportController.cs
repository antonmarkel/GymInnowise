using GymInnowise.ReportService.API.Controllers.Base;
using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Perstistence.Models.Entities;
using GymInnowise.Shared.Reports;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.ReportService.API.Controllers
{
    [ApiController]
    [Route("api/training-report")]
    public class TrainingReportController : ReportControllerBase<TrainingReport, TrainingReportEntity>
    {
        public TrainingReportController(IReportService<TrainingReport, TrainingReportEntity> reportService,
            IReportFileGenerator<TrainingReport> fileGenerator) : base(reportService, fileGenerator)
        {
        }
    }
}
