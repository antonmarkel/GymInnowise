using AutoMapper;
using GymInnowise.GymService.Persistence.Models.Dtos;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Shared.Dtos.Requests.Creates;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;
using GymInnowise.GymService.Shared.Dtos.Responses.Gets;

namespace GymInnowise.GymService.Logic.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateEventsMappings();
            CreateGymsMappings();
        }

        private void CreateEventsMappings()
        {
            CreateMap<CreateGymEventRequest, GymEventEntity>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateGymEventRequest, GymEventEntity>();
            CreateMap<GymEventEntity, GetGymEventResponse>();
        }

        private void CreateGymsMappings()
        {
            CreateMap<CreateGymRequest, GymEntity>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateGymRequest, GymEntity>();
            CreateMap<GymEntity, GetGymDetailsResponse>();
            CreateMap<GymPreviewDto, GetGymPreviewResponse>();
        }
    }
}