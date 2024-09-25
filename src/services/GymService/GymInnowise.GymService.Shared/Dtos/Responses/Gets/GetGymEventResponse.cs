using GymInnowise.GymService.Shared.Dtos.Abstract;

namespace GymInnowise.GymService.Shared.Dtos.Responses.Gets
{
    public class GetGymEventResponse : GymEventDtoBase
    {
        public Guid Id { get; set; }
    }
}
