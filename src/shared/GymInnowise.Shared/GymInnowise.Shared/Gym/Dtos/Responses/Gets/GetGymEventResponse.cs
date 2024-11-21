using GymInnowise.Shared.Gym.Dtos.Abstract;

namespace GymInnowise.Shared.Gym.Dtos.Responses.Gets
{
    public class GetGymEventResponse : GymEventDtoBase
    {
        public Guid Id { get; set; }
    }
}