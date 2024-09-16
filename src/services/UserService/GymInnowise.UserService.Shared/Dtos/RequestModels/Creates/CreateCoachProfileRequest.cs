namespace GymInnowise.UserService.Shared.Dtos.RequestModels.Creates
{
    public class CreateCoachProfileRequest : CreateClientProfileRequest
    {
        public decimal CostPerHour { get; set; }
    }
}
