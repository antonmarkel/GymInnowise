using AutoMapper;
using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.ReportService.Perstistence.Reporisitories.Interfaces;
using GymInnowise.Shared.Reports.Interfaces;
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

        public ReportService(IReportRepository<TReportEntity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreateReportAsync(TReport report, Guid reportId)
        {
            var entity = _mapper.Map<TReportEntity>(report);
            entity.Id = reportId;
            await _repo.AddReportAsync(entity);
        }

        public async Task<OneOf<TReport, NotFound>> GetReportAsync(Guid reportId)
        {
            var reportEntity = await _repo.GetReportAsync(reportId);
            if (reportEntity is null)
            {
                return new NotFound();
            }

            return _mapper.Map<TReport>(reportEntity);
        }
    }
}