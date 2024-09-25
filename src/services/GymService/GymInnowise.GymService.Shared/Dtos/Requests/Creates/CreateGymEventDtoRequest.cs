using GymInnowise.GymService.Shared.Dtos.Abstract;

namespace GymInnowise.GymService.Shared.Dtos.Requests.Creates
{
    public class CreateGymEventDtoRequest : GymEventDtoBase
    {
        public Guid GymId { get; set; }
    }
}
