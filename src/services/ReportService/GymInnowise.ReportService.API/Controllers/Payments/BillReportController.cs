using GymInnowise.ReportService.API.Controllers.Base;
using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Perstistence.Models.Entities;
using GymInnowise.Shared.Reports.Payment;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.ReportService.API.Controllers.Payments
{
    [ApiController]
    [Route("api/reports/bill")]
    public class BillReportController : ReportControllerBase<BillReport, BillReportEntity>
    {
        public BillReportController(IReportService<BillReport, BillReportEntity> reportService,
            IReportFileGenerator<BillReport> fileGenerator) : base(reportService, fileGenerator)
        {
        }
    }
}
