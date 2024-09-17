using GymInnowise.GymService.Shared.Dtos.Abstract;

namespace GymInnowise.GymService.Shared.Dtos.Responses
{
    public class GetGymEventResponse : GymEventBaseDto
    {
        public Guid Id { get; set; }
    }
}
