using GymInnowise.GymService.Shared.Dtos.Abstract;

namespace GymInnowise.GymService.Shared.Dtos.Requests.Creates
{
    public class CreateGymEventRequest : GymEventBaseDto
    {
        public Guid GymId { get; set; }
    }
}
