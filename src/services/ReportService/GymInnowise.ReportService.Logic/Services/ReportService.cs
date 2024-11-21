using AutoMapper;
using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.ReportService.Perstistence.Reporisitories.Interfaces;
using GymInnowise.Shared.Reports.Interfaces;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace GymInnowise.ReportService.Logic.Services
{
    public class ReportService<TReport, TReportEntity> : IReportService<TReport, TReportEntity>
        where TReport : IReport
        where TReportEntity : class, TReport, IReportEntity
    {
        private readonly IReportRepository<TReportEntity> _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<ReportService<TReport, TReportEntity>> _logger;

        public ReportService(IReportRepository<TReportEntity> repo, IMapper mapper,
            ILogger<ReportService<TReport, TReportEntity>> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateReportAsync(TReport report, Guid reportId)
        {
            var entity = _mapper.Map<TReportEntity>(report);
            entity.Id = reportId;
            _logger.LogInformation("Report was created: @{reportId}", reportId);
            await _repo.AddReportAsync(entity);
        }

        public async Task<OneOf<TReport, NotFound>> GetReportAsync(Guid reportId)
        {
            var reportEntity = await _repo.GetReportAsync(reportId);
            if (reportEntity is null)
            {
                _logger.LogWarning("Report was not found: @{reportId}", reportId);

                return new NotFound();
            }

            _logger.LogInformation("Report was successfully retrieved: @{reportId}", reportId);

            return _mapper.Map<TReport>(reportEntity);
        }

        public async Task<OneOf<Success, NotFound>> UpdateReportAsync(Guid reportId, TReport report)
        {
            var reportEntity = await _repo.GetReportAsync(reportId);
            if (reportEntity is null)
            {
                _logger.LogWarning("Report was not found: @{reportId}", reportId);

                return new NotFound();
            }

            _logger.LogInformation("Report was successfully retrieved: @{reportId}", reportId);
            reportEntity = _mapper.Map<TReportEntity>(reportEntity);
            await _repo.UpdateReportAsync(reportEntity);
            _logger.LogInformation("Report was successfully updated: @{reportId}", reportId);

            return new Success();
        }
    }
}