using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.Shared.Reports.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.ReportService.API.Controllers.Base
{
    public class ReportControllerBase<TReport, TReportEntity> : ControllerBase
        where TReport : IReport
        where TReportEntity : class, TReport, IReportEntity
    {
        private readonly IReportService<TReport, TReportEntity> _reportService;
        private readonly IReportFileGenerator<TReport> _fileGenerator;

        public ReportControllerBase(IReportService<TReport, TReportEntity> reportService,
            IReportFileGenerator<TReport> fileGenerator)
        {
            _fileGenerator = fileGenerator;
            _reportService = reportService;
        }

        [ActionName(nameof(GetReportByIdAsync))]
        [HttpGet("{reportId}")]
        public async Task<IActionResult> GetReportByIdAsync([FromRoute] Guid reportId)
        {
            var result = await _reportService.GetReportAsync(reportId);

            return result.Match<IActionResult>(
                report => Ok(report),
                _ => NotFound()
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateReportAsync([FromBody] TReport report)
        {
            Guid reportId = Guid.NewGuid();
            await _reportService.CreateReportAsync(report, reportId);

            var contollerName = GetType().Name.Replace("Controller", "");
            var actionName = nameof(GetReportByIdAsync);

            return CreatedAtAction(
                actionName,
                contollerName,
                new { reportId },
                reportId
            );
        }


        [HttpPost("generate/{reportId}")]
        public async Task<IActionResult> GenerateReportAsync([FromRoute] Guid reportId)
        {
            var reportResult = await _reportService.GetReportAsync(reportId);
            if (reportResult.IsT1)
            {
                return NotFound();
            }

            var result = await _fileGenerator.GenerateReportAsync(reportResult.AsT0);

            //c2b80888-bb13-4fbf-b749-fddd4824df15
            return result.Match<IActionResult>(
                stream => File(stream, "application/pdf"),
                _ => BadRequest("Generation Failed (Html convert)"),
                _ => BadRequest("Generation Failed (Pdf convert)")
            );
        }
    }
}
