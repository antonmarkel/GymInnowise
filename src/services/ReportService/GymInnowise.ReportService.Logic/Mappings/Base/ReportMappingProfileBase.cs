using AutoMapper;
using GymInnowise.ReportService.Perstistence.Models.Interfaces;
using GymInnowise.Shared.Reports.Interfaces;

namespace GymInnowise.ReportService.Logic.Mappings.Base
{
    public abstract class ReportMappingProfileBase<TReport, TReportEntity> : Profile
        where TReport : IReport
        where TReportEntity : class, TReport, IReportEntity
    {
        public ReportMappingProfileBase()
        {
            CreateMap<TReport, TReportEntity>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<TReportEntity, TReport>();
        }
    }
}
