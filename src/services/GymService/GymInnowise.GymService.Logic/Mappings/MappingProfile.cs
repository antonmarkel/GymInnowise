using AutoMapper;
using GymInnowise.GymService.Persistence.Models.Dtos;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.Shared.Gym.Dtos.Requests.Creates;
using GymInnowise.Shared.Gym.Dtos.Requests.Updates;
using GymInnowise.Shared.Gym.Dtos.Responses.Gets;

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
            CreateMap<CreateGymEventDtoRequest, GymEventEntity>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateGymEventDtoRequest, GymEventEntity>();
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