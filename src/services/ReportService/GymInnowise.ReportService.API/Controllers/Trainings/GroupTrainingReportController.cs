using GymInnowise.ReportService.API.Controllers.Base;
using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Perstistence.Models.Entities;
using GymInnowise.Shared.Reports.Trainings;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.ReportService.API.Controllers.Trainings
{
    [ApiController]
    [Route("api/reports/group-training")]
    public class GroupTrainingReportController : ReportControllerBase<GroupTrainingReport, GroupTrainingReportEntity>
    {
        public GroupTrainingReportController(
            IReportService<GroupTrainingReport, GroupTrainingReportEntity> reportService,
            IReportFileGenerator<GroupTrainingReport> fileGenerator) : base(reportService, fileGenerator)
        {
        }
    }
}