using GymInnowise.Shared.Gym.Dtos.Abstract;

namespace GymInnowise.Shared.Gym.Dtos.Requests.Creates
{
    public class CreateGymEventDtoRequest : GymEventDtoBase
    {
        public Guid GymId { get; set; }
    }
}